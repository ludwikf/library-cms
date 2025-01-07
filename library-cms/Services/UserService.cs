using LibraryManagementWeb.Models;
using System.Text.Json;

namespace LibraryManagementWeb.Services
{
    public class UserService
    {
        private readonly string _userFilePath = "zdata-users.json";

        public List<User> GetAllUsers()
        {
            if (!File.Exists(_userFilePath))
                return new List<User>();

            var json = File.ReadAllText(_userFilePath);
            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }

        public User? GetUserByUsername(string username)
        {
            var users = GetAllUsers();
            return users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

  
        public User? ValidateUser(string username, string password)
        {
            try
            {
                var user = GetUserByUsername(username);
                Console.WriteLine($"Validating user: {username}");
                Console.WriteLine($"User found: {user != null}");
                
                if (user != null)
                {
                    Console.WriteLine($"Password check: {user.Password == password}");
                }
                
                return (user != null && user.Password == password) ? user : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error validating user: {ex.Message}");
                return null;
            }
        }

      
        public void AddUser(User newUser)
        {
            var users = GetAllUsers();
            users.Add(newUser);

            var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_userFilePath, json);
        }
    }
}
