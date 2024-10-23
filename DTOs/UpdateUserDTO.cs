namespace StudentManagementSystem.DTOs
{
    public class UpdateUserDTO
    {
        public string FirstName { get; set; } = string.Empty;

        public string MiddleName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public int Department { get; set; }

        public string EmailAddress { get; set; } = string.Empty;
    }
}
