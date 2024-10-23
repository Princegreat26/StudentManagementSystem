using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class AdminManagement
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
    }
}
