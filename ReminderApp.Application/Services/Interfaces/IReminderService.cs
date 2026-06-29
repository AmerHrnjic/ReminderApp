using ReminderApp.Common.Dtos.Application;
using ReminderApp.Domain.Entities;

namespace ReminderApp.Application.Services.Interfaces
{
    public interface IReminderService
    {
        public Task<IEnumerable<ReminderDto>> GetAllRemindersAsync(CancellationToken cancellationToken);
        public Task<ReminderDto?> CreateReminderAsync(Reminder reminder, CancellationToken cancellationToken);
    }
}
