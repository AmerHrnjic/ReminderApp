namespace ReminderApp.Application.Jobs
{
    public interface IReminderJob
    {
        Task ExecuteJob(Guid reminderId, string to, string subject, string body, CancellationToken cancellationToken);
    }
}
