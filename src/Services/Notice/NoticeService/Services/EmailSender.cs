using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace Together.Notice.Services
{
    public class EmailSender
        : IEmailSender
    {
        private readonly SendGridClient _client;
        private readonly EmailSettings _settings;
        public EmailSender(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
            _client = new SendGridClient(_settings.ApiKey);
        }

        public async Task<bool> Send(string to, string subject, string plainTextContent, string htmlContent)
        {
            var from = new EmailAddress(_settings.From, _settings.Name);
            var recipient = new EmailAddress(to);
            var message = MailHelper.CreateSingleEmail(from, recipient, subject, plainTextContent, htmlContent);
            var response = await _client.SendEmailAsync(message);
            return response.StatusCode == System.Net.HttpStatusCode.Accepted;
        }
    }
}

