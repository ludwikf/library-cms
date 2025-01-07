using LibraryManagementWeb.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Hosting;

namespace LibraryManagementWeb.Services
{
    public class BookService
    {
        private readonly string _filePath;

        public BookService(IWebHostEnvironment webHostEnvironment)
        {
            _filePath = Path.Combine(webHostEnvironment.ContentRootPath, "zdata-books.json");
        }

        public List<Book> GetAllBooks()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Book>();
            }

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
        }

        public void AddBook(Book newBook)
        {
            try
            {
                var books = GetAllBooks();
                books.Add(newBook);
                var json = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding book: {ex.Message}. File path: {_filePath}", ex);
            }
        }

        public List<Book> GetReservedBooks(string username)
        {
            var allBooks = GetAllBooks();
            return allBooks.Where(b => b.ReservedBy == username).ToList();
        }

        public void ReserveBook(string bookTitle, string username)
        {
            var books = GetAllBooks();
            var book = books.FirstOrDefault(b => b.Title == bookTitle);
            
            if (book == null)
            {
                throw new Exception("Book not found.");
            }
            
            if (!book.IsAvailable)
            {
                throw new Exception("Book is not available for reservation.");
            }

            book.IsAvailable = false;
            book.ReservedBy = username;
            
            SaveBooks(books);
        }

        public void ReturnBook(string bookTitle, string username)
        {
            var books = GetAllBooks();
            var book = books.FirstOrDefault(b => b.Title == bookTitle && b.ReservedBy == username);
            
            if (book == null)
            {
                throw new Exception("Book not found or not reserved by you.");
            }

            book.IsAvailable = true;
            book.ReservedBy = null;
            
            SaveBooks(books);
        }

        public void AdminRemoveReservation(string bookTitle)
        {
            try
            {
                var books = GetAllBooks();
                var book = books.FirstOrDefault(b => b.Title == bookTitle);
                
                if (book == null)
                {
                    throw new Exception("Book not found.");
                }

                book.IsAvailable = true;
                book.ReservedBy = null;
                
                var json = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error removing reservation: {ex.Message}. File path: {_filePath}", ex);
            }
        }

        public void RemoveBook(string bookTitle)
        {
            try
            {
                var books = GetAllBooks();
                var book = books.FirstOrDefault(b => b.Title == bookTitle);
                
                if (book == null)
                {
                    throw new Exception("Book not found.");
                }

                if (!book.IsAvailable)
                {
                    throw new Exception("Cannot remove book while it is reserved.");
                }

                books.Remove(book);
                
             
                var json = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error removing book: {ex.Message}. File path: {_filePath}", ex);
            }
        }

        private void SaveBooks(List<Book> books)
        {
            var json = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
    }
}
