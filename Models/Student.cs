using MimeKit.Tnef;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; } 

        public string? Address {  get; set; } 

        public string  PhoneNumber { get; set; }

        public string? EmailAddress { get; set; }

        public string RegistrationNumber { get; set; } = string.Empty;

        [ForeignKey("Departments")]
        public int DepartmentsId { get; set; }

        public virtual Department Department { get; set; }

    }
}
