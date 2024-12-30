using LibraryManagementWeb.Models;
using System.Text.Json;
using System.IO;

namespace LibraryManagementWeb.Services
{
    public class UserService
    {
        private const string UserFilePath = "users.json"; // Path to the JSON file

        // Load users from the JSON file
        private List<User> LoadUsers()
        {
            if (File.Exists(UserFilePath))
            {
                var json = File.ReadAllText(UserFilePath);
                return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
            return new List<User>();
        }

        // Save users to the JSON file
        private void SaveUsers(List<User> users)
        {
            var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(UserFilePath, json);
        }

        // Validate the user's credentials during login
        public User ValidateUser(string username, string password)
        {
            var users = LoadUsers();
            return users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        // Get user by username (for registration to check if it exists)
        public User GetUserByUsername(string username)
        {
            var users = LoadUsers();
            return users.FirstOrDefault(u => u.Username == username);
        }

        // Add a new user (register new user)
        public void AddUser(User user)
        {
            var users = LoadUsers();
            users.Add(user);
            SaveUsers(users);
        }
    }
}
