using System.Net;
using System.Net.Mail;
using Contas.Core.Interfaces.Services;
using Contas.Infrastructure.Objects;
using Microsoft.Extensions.Options;

namespace Contas.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _settings;

    public EmailService(IOptions<EmailSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        using var client = new SmtpClient(_settings.Host, _settings.Port)
        {
            EnableSsl = _settings.EnableSsl
        };

        if (!string.IsNullOrEmpty(_settings.Username))
        {
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_settings.Username, _settings.Password);
        }

        var message = new MailMessage
        {
            From = new MailAddress(_settings.FromAddress, _settings.FromName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        message.To.Add(to);

        await client.SendMailAsync(message);
    }
}
