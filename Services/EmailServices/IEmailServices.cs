namespace StudentManagementSystem.Services.EmailServices
{
    public interface IEmailServices
    {
        Task SendEmailAsync(string email, string subject, string body);

        string GetEmailTemplate(string firstName, string lastName, string registrationNumber, string department);
    }
}