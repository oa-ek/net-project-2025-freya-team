using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

namespace Freya.Helpers
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var fromAddress = new MailAddress(_emailSettings.SmtpUsername, "Freya");
            var toAddress = new MailAddress(toEmail);
            using (var smtp = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort))
            {
                smtp.Credentials = new NetworkCredential(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword);
                smtp.EnableSsl = true;

                var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                await smtp.SendMailAsync(message);
            }
        }
    }
}
