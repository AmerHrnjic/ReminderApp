using Microsoft.EntityFrameworkCore;
using ReminderApp.Domain.Entities;

namespace ReminderApp.Infrastructure
{
    public class ReminderAppDbContext : DbContext
    {
        public ReminderAppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Reminder> Reminders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Automatically loads all Fluent API configs from this assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReminderAppDbContext).Assembly);
        }
    }
}
