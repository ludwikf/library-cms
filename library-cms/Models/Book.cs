using System;

namespace LibraryManagementWeb.Models
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public bool IsAvailable { get; set; }
        public string? ReservedBy { get; set; }
    }
}
