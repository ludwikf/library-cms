using LibraryManagementWeb.Models;
using LibraryManagementWeb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementWeb.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserService _userService;

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public string? ErrorMessage { get; set; }

        public LoginModel(UserService userService)
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

            // Validate user credentials
            var user = _userService.ValidateUser(Username, Password);
            if (user == null)
            {
                ErrorMessage = "Invalid username or password.";
                return Page();
            }

            // Set session
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);

            // Redirect to home page
            var sessionUsername = HttpContext.Session.GetString("Username");
            var sessionRole = HttpContext.Session.GetString("Role");
            Console.WriteLine($"Session Username: {sessionUsername}, Role: {sessionRole}");

            // Redirect to home page
            return RedirectToPage("/Index");
        }
    }
}
