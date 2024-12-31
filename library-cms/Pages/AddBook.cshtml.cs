using LibraryManagementWeb.Models;
using LibraryManagementWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementWeb.Pages
{
    public class AddBookModel : PageModel
    {
        private readonly BookService _bookService;

        [BindProperty]
        public Book Book { get; set; }

        public string ErrorMessage { get; set; }

        public AddBookModel(BookService bookService)
        {
            _bookService = bookService;
        }

        public void OnGet()
        {
            // This is for the initial load. We can add any logic needed here.
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Please fill out all fields correctly.";
                return Page();
            }

            // Add the new book to the JSON file
            _bookService.AddBook(Book);

            // Redirect to the books list or wherever you want after adding the book
            return RedirectToPage("/Books");
        }
    }
}
