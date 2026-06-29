using Microsoft.EntityFrameworkCore;
using ReminderApp.Application.Repositories.Interfaces;
using ReminderApp.Domain.Entities;

namespace ReminderApp.Infrastructure.Repositories.Implementations
{
    public class ReminderRepository(ReminderAppDbContext dbContext) : IReminderRepository
    {
        public async Task<Reminder?> AddReminderAsync(Reminder reminder, CancellationToken cancellationToken)
        {
            var createdReminder = (await dbContext.Reminders
            .AddAsync(reminder, cancellationToken))
            .Entity;

            int addedCount = await dbContext.SaveChangesAsync(cancellationToken);

            if (addedCount == 0)
            {
                return null;
            }

            return createdReminder;
        }

        public async Task<IEnumerable<Reminder>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbContext.Reminders
                .ToListAsync(cancellationToken);
        }

        public async Task<Reminder?> GetReminderAsync(Guid id, CancellationToken cancellationToken)
        {
            return await dbContext.Reminders
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
