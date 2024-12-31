using LibraryManagementWeb.Models;
using System.Text.Json;

namespace LibraryManagementWeb.Services
{
    public class UserService
    {
        private readonly string _userFilePath = "zdata-users.json";

        // Retrieve all users from the JSON file
        public List<User> GetAllUsers()
        {
            if (!File.Exists(_userFilePath))
                return new List<User>();

            var json = File.ReadAllText(_userFilePath);
            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }

        // Get a user by username
        public User? GetUserByUsername(string username)
        {
            var users = GetAllUsers();
            return users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        // Validate user credentials
        public User? ValidateUser(string username, string password)
        {
            var user = GetUserByUsername(username);
            return (user != null && user.Password == password) ? user : null;
        }

        // Add a new user to the JSON file
        public void AddUser(User newUser)
        {
            var users = GetAllUsers();
            users.Add(newUser);

            var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_userFilePath, json);
        }
    }
}
