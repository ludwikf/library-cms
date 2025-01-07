using LibraryManagementWeb.Models;
using LibraryManagementWeb.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementWeb.Pages
{
    public class BooksModel : PageModel
    {
        private readonly BookService _bookService;

        public List<Book> Books { get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }

        public BooksModel(BookService bookService)
        {
            _bookService = bookService;
        }

        public void OnGet()
        {
            Books = _bookService.GetAllBooks();
        }

        public IActionResult OnPostReserve(string bookTitle)
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToPage("/Login");
            }

            try
            {
                _bookService.ReserveBook(bookTitle, username);
                TempData["SuccessMessage"] = $"Successfully reserved '{bookTitle}'";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToPage();
        }

        public IActionResult OnPostRemove(string bookTitle)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
            {
                return RedirectToPage("/Index");
            }

            try
            {
                _bookService.RemoveBook(bookTitle);
                TempData["SuccessMessage"] = $"Successfully removed '{bookTitle}'";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToPage();
        }
    }
}
