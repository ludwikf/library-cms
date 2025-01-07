using LibraryManagementWeb.Models;
using LibraryManagementWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementWeb.Pages
{
    public class AdminReservedBooksModel : PageModel
    {
        private readonly BookService _bookService;

        public List<Book> ReservedBooks { get; set; } = new List<Book>();

        public AdminReservedBooksModel(BookService bookService)
        {
            _bookService = bookService;
        }

        public IActionResult OnGet()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
            {
                return RedirectToPage("/Index");
            }

            ReservedBooks = _bookService.GetAllBooks().Where(b => !b.IsAvailable).ToList();
            return Page();
        }

        public IActionResult OnPostRemoveReservation(string bookTitle)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
            {
                return RedirectToPage("/Index");
            }

            try
            {
                _bookService.AdminRemoveReservation(bookTitle);
                TempData["SuccessMessage"] = $"Successfully removed reservation for '{bookTitle}'";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToPage();
        }
    }
}