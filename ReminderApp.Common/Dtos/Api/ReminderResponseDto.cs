namespace ReminderApp.Common.Dtos.Api
{
    public record ReminderResponseDto(
        string Id,
        string Message,
        string SendAt,
        string Status,
        string Email
    );
}
