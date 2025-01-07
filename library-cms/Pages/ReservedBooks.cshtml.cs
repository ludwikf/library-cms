using LibraryManagementWeb.Models;
using LibraryManagementWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementWeb.Pages
{
    public class ReservedBooksModel : PageModel
    {
        private readonly BookService _bookService;

        public List<Book> ReservedBooks { get; set; } = new List<Book>();

        public ReservedBooksModel(BookService bookService)
        {
            _bookService = bookService;
        }

        public IActionResult OnGet()
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToPage("/Login");
            }

            ReservedBooks = _bookService.GetReservedBooks(username);
            return Page();
        }

        public IActionResult OnPostReturn(string bookTitle)
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToPage("/Login");
            }

            try
            {
                _bookService.ReturnBook(bookTitle, username);
                TempData["SuccessMessage"] = $"Successfully returned '{bookTitle}'";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToPage();
        }
    }
}