using Microsoft.Extensions.Logging;
using ReminderApp.Application.Repositories.Interfaces;
using ReminderApp.Application.Services.Interfaces;

namespace ReminderApp.Application.Jobs
{
    public class ReminderJob(
        IEmailService emailService,
        IReminderRepository reminderRepository,
        ILogger<ReminderJob> logger
        ) : IReminderJob
    {
        public async Task ExecuteJob(Guid reminderId, string to, string subject, string body, CancellationToken cancellationToken)
        {
            var reminder = await reminderRepository.GetReminderAsync(reminderId, cancellationToken);

            if (reminder == null)
            {
                logger.LogError("Reminder not found. Exiting the job.");
                return;
            }

            var emailSent = await emailService.SendEmailAsync(to, subject, body, cancellationToken);

            if (!emailSent)
            {
                reminder.SetStatus(Common.Enums.ReminderStatus.Failed);
            }
            else
            {
                reminder.SetStatus(Common.Enums.ReminderStatus.Sent);
            }

            await reminderRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
