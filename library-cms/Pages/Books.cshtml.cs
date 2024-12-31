using LibraryManagementWeb.Models;
using LibraryManagementWeb.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementWeb.Pages
{
    public class BooksModel : PageModel
    {
        private readonly BookService _bookService;

        public List<Book> Books { get; set; }

        public BooksModel(BookService bookService)
        {
            _bookService = bookService;
        }

        public void OnGet()
        {
            // Retrieve all books from the JSON file
            Books = _bookService.GetAllBooks();
        }
    }
}
