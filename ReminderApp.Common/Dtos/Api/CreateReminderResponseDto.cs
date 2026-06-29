namespace ReminderApp.Common.Dtos.Api
{
    public record CreateReminderResponseDto(
        string Id,
        string Status,
        string SendAt
    );
}
