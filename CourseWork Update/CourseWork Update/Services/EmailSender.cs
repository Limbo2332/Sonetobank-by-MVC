using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace CourseWork_Update.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var sendGridKey = @"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";
            return Execute(sendGridKey, subject, htmlMessage, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("vorobiovalosha@gmail.com", "Fitrianingrum via SendGrid"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}