using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using ReminderApp.Application.Services.Interfaces;

namespace ReminderApp.Infrastructure.Services
{
    public class EmailService(
        IOptions<EmailSettings> settings,
        ILogger<EmailService> logger,
        ReminderAppDbContext dbContext) : IEmailService
    {
        public async Task<bool> SendEmailAsync(
              string to,
              string subject,
              string htmlBody,
              CancellationToken cancellationToken)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(settings.Value.FromEmail, settings.Value.FromEmail));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;

            message.Body = new TextPart("html")
            {
                Text = htmlBody
            };

            using var client = new SmtpClient();

            try
            {
                await client.ConnectAsync(settings.Value.Host, settings.Value.Port, MailKit.Security.SecureSocketOptions.StartTls, cancellationToken);
                await client.AuthenticateAsync(settings.Value.UserName, settings.Value.Password, cancellationToken);

                await client.SendAsync(message, cancellationToken);
                await client.DisconnectAsync(true, cancellationToken);
            }
            catch (AuthenticationException)
            {
                logger.LogError("SMTP authentication failed.");
                return false;
            }
            catch (SmtpCommandException ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }

            return true;
        }
    }
}
