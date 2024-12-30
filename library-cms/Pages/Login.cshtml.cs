using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LibraryManagementWeb.Services;
using LibraryManagementWeb.Models;

namespace LibraryManagementWeb.Pages;

public class LoginModel : PageModel
{
    private readonly UserService _userService;

    public LoginModel(UserService userService)
    {
        _userService = userService;
    }

    [BindProperty]
    public string Username { get; set; }

    [BindProperty]
    public string Password { get; set; }

    public string ErrorMessage { get; private set; }

    public IActionResult OnPost()
    {
        var user = _userService.ValidateUser(Username, Password);
        if (user != null)
        {
            // Login successful
            return RedirectToPage("Index");
        }

        // Login failed
        ErrorMessage = "Invalid username or password.";
        return Page();
    }
}
