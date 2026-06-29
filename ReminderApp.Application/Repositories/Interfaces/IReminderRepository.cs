using ReminderApp.Domain.Entities;

namespace ReminderApp.Application.Repositories.Interfaces
{
    public interface IReminderRepository
    {
        public Task<Reminder?> AddReminderAsync(Reminder reminder, CancellationToken cancellationToken);
        public Task<IEnumerable<Reminder>> GetAllAsync(CancellationToken cancellationToken);
        public Task<Reminder?> GetReminderAsync(Guid id, CancellationToken cancellationToken);
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
