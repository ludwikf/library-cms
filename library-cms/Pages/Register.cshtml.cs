using LibraryManagementWeb.Models;
using LibraryManagementWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementWeb.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserService _userService;

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public string? ErrorMessage { get; set; } // Add this property

        public RegisterModel(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Username and password are required.";
                return Page();
            }

            // Check if the user already exists
            var existingUser = _userService.GetUserByUsername(Username);
            if (existingUser != null)
            {
                ErrorMessage = "User already exists.";
                return Page();
            }

            // Add new user
            var newUser = new User
            {
                Username = Username,
                Password = Password
            };
            _userService.AddUser(newUser);

            TempData["SuccessMessage"] = "Registration successful. Please log in.";
            return RedirectToPage("/Login");
        }
    }
}
