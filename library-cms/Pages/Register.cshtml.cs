using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LibraryManagementWeb.Services;
using LibraryManagementWeb.Models;
using System.Text.Json;
using System.IO;

namespace LibraryManagementWeb.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserService _userService;

        public RegisterModel(UserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string Role { get; set; }

        public string ErrorMessage { get; private set; }

        public IActionResult OnPost()
        {
            // Check if the user already exists
            var existingUser = _userService.GetUserByUsername(Username);
            if (existingUser != null)
            {
                ErrorMessage = "Username is already taken.";
                return Page();
            }

            // Create a new user and save it to the JSON file
            var newUser = new User
            {
                Username = Username,
                Password = Password, // In real apps, hash passwords
                Role = Role
            };

            _userService.AddUser(newUser);

            // Redirect to Login page or Index page after successful registration
            return RedirectToPage("Login");
        }
    }
}
