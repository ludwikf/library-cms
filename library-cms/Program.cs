using LibraryManagementWeb.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSingleton<BookService>(); 
builder.Services.AddSingleton<UserService>();


// Add session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true; // Mitigate XSS attacks
    options.Cookie.IsEssential = true; // Required for session to work without consent
});

// Optionally, add HttpContextAccessor if needed elsewhere
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Enable HTTPS redirection
app.UseHttpsRedirection();

// Enable static files
app.UseStaticFiles();

// Enable routing
app.UseRouting();

// Enable session before authorization
app.UseSession();

// Enable authorization
app.UseAuthorization();

// Map Razor Pages
app.MapRazorPages();

app.Run();
