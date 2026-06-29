using ReminderApp.Application.Jobs;
using ReminderApp.Application.Repositories.Interfaces;
using ReminderApp.Application.Services.Interfaces;
using ReminderApp.Common.Dtos.Application;
using ReminderApp.Domain.Entities;

namespace ReminderApp.Application.Services
{
    public class ReminderService(
            IReminderRepository reminderRepository,
            IJobScheduler jobScheduler
        ) : IReminderService
    {
        public async Task<ReminderDto?> CreateReminderAsync(Reminder reminder, CancellationToken cancellationToken)
        {

            var addedReminder = await reminderRepository.AddReminderAsync(reminder, cancellationToken);

            if (addedReminder is null)
            {
                return null;
            }

            jobScheduler.Schedule<IReminderJob>(
                (job) => job.ExecuteJob(addedReminder.Id, addedReminder.Email, "Reminder", addedReminder.Message, cancellationToken),
                addedReminder.SendAt
            );

            return new ReminderDto(addedReminder.Id, addedReminder.Message, addedReminder.SendAt, addedReminder.Status, addedReminder.Email);
        }

        public async Task<IEnumerable<ReminderDto>> GetAllRemindersAsync(CancellationToken cancellationToken)
        {
            return (await reminderRepository.GetAllAsync(cancellationToken))
                .Select(r => new ReminderDto(r.Id, r.Message, r.SendAt, r.Status, r.Email))
                .ToList();
        }
    }
}
