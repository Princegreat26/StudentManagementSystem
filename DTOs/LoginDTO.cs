using Azure.Core;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.DTOs
{
    public class LoginDTO
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}