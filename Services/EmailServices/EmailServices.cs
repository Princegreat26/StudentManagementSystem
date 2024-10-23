using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace StudentManagementSystem.Services.EmailServices
{
    public class EmailServices : IEmailServices
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;

        public EmailServices(IOptions<EmailSettings> emailSettings)
        {
            _smtpServer = emailSettings.Value.SmtpServer;
            _smtpPort = emailSettings.Value.SmtpPort;
            _smtpUser = emailSettings.Value.SmtpUser;
            _smtpPass = emailSettings.Value.SmtpPass;
        }

        public async Task SendEmailAsync(string emailAddress, string subject, string body)
        {
            using (var message = new MailMessage())
            {
                message.From = new MailAddress(_smtpUser, "Net University");
                message.To.Add(new MailAddress(emailAddress));
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                using (var smtpClient = new SmtpClient(_smtpServer, _smtpPort))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
                    smtpClient.EnableSsl = true;
                    await smtpClient.SendMailAsync(message);
                }
            }
        }

        public string GetEmailTemplate(string firstName, string lastName, string registrationNumber, string department)
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var templatePath = Path.Combine(baseDirectory, "EmailTemplate.html");
            //var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates", "EmailTemplate.html");
            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException("Email template file not found.", templatePath);
            }

            var template = File.ReadAllText(templatePath);
            template = template.Replace("{FirstName}", firstName)
                               .Replace("{LastName}", lastName)
                               .Replace("{RegistrationNumber}", registrationNumber)
                               .Replace("{Department}", department);
            return template;
        }
    }
}

//C:\Users\greatpaul\Desktop\Projects\EmailTest\EmailTest\bin\Debug\net8.0\
