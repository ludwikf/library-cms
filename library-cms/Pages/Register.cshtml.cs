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
        [BindProperty]
        public string ConfirmPassword { get; set; }

        public string? ErrorMessage { get; set; }

        public RegisterModel(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                ErrorMessage = "All fields are required.";
                return Page();
            }

            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Passwords do not match.";
                return Page();
            }

            var existingUser = _userService.GetUserByUsername(Username);
            if (existingUser != null)
            {
                ErrorMessage = "User already exists.";
                return Page();
            }

         
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
