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
            Console.WriteLine($"Login attempt - Username: {Username}");

            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Username and password are required.";
                return Page();
            }

    
            var user = _userService.ValidateUser(Username, Password);
            
        
            Console.WriteLine($"User validation result: {(user != null ? "Success" : "Failed")}");
            
            if (user == null)
            {
                ErrorMessage = "Invalid username or password.";
                return Page();
            }

         
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);

           
            var sessionUsername = HttpContext.Session.GetString("Username");
            var sessionRole = HttpContext.Session.GetString("Role");
            Console.WriteLine($"Session set - Username: {sessionUsername}, Role: {sessionRole}");

            return RedirectToPage("/Index");
        }
    }
}
