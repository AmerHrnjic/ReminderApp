using ReminderApp.Common.Enums;

namespace ReminderApp.Domain.Entities
{
    public class Reminder
    {
        public Guid Id { get; set; }
        public required string Message { get; set; }
        public required DateTime SendAt { get; set; }
        public required ReminderStatus Status { get; set; }
        public required string Email { get; set; }

        public void SetStatus(ReminderStatus status) => Status = status;
    }
}
