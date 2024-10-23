namespace StudentManagementSystem.DTOs
{
    public class DeleteUserDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string MiddleName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public long PhoneNumber { get; set; }

        public string EmailAddress { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        public string RegisterationNumber { get; set; } = string.Empty;
    }
}
