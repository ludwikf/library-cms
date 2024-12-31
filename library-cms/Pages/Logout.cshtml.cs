using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryManagementWeb.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Clear session data
            HttpContext.Session.Clear();

            // Optionally, you can remove specific session keys
            // HttpContext.Session.Remove("Username");
            // HttpContext.Session.Remove("Role");

            // Redirect to home page or login page
            return RedirectToPage("/Index");
        }
    }
}
