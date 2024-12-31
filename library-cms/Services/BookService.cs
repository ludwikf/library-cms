using LibraryManagementWeb.Models;
using System.Text.Json;

namespace LibraryManagementWeb.Services
{
    public class BookService
    {
        private readonly string _booksFilePath = "zdata-books.json";  

        // Retrieve all books from the JSON file
        public List<Book> GetAllBooks()
        {
            if (!File.Exists(_booksFilePath))
                return new List<Book>();

            var json = File.ReadAllText(_booksFilePath);
            return JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
        }

        // Add a new book to the JSON file
        public void AddBook(Book newBook)
        {
            var books = GetAllBooks();
            books.Add(newBook);

            var json = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_booksFilePath, json);
        }
    }
}
