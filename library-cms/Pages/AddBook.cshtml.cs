using LibraryManagementWeb.Models;
using LibraryManagementWeb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Text.Json;

namespace LibraryManagementWeb.Pages
{
    public class AddBookModel : PageModel
    {
        private readonly BookService _bookService;

        [BindProperty]
        public Book NewBook { get; set; }

        public string? ErrorMessage { get; set; }

        public AddBookModel(BookService bookService)
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
            return Page();
        }

        [HttpPost]
        public IActionResult OnPost()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
            {
                return RedirectToPage("/Index");
            }

            if (!ModelState.IsValid || NewBook == null)
            {
                ErrorMessage = "Please fill in all required fields.";
                return Page();
            }

            try
            {
                NewBook.IsAvailable = true;
                _bookService.AddBook(NewBook);
                return RedirectToPage("/Books");
            }
            catch (Exception ex)
            {
                ErrorMessage = "An error occurred while adding the book.";
                return Page();
            }
        }
    }
}
