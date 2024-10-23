namespace StudentManagementSystem.DTOs
{
    public class UserDTO 
    {
        public string FirstName { get; set; } = string.Empty;

        public string MiddleName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        public string RegistrationNumber { get; set; } = string.Empty;

        public string Abbreviation { get; set; } = string.Empty;

        public string EmailAddress { get; set; } = string.Empty;
    }

    public class ResponseDTO 
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string MiddleName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string EmailAddress { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        public string RegisterationNumber {  get; set; } = string.Empty;
    }

    public class PrivateDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string MiddleName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string EmailAddress { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        public string RegisterationNumber { get; set; } = string.Empty;

        ///entity or table entity models
        ///user input DTOs
        ///user response response models
    }

    public class DepartmentDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Abbreviation { get; set; } = string.Empty;
    }
}
