using ReminderApp.Common.Enums;

namespace ReminderApp.Common.Dtos.Application
{
    public record ReminderDto(
        Guid Id,
        string Message,
        DateTime SendAt,
        ReminderStatus Status,
        string Email
    );
}
