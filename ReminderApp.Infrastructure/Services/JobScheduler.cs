using Hangfire;
using ReminderApp.Application.Services.Interfaces;
using System.Linq.Expressions;

namespace ReminderApp.Infrastructure.Services
{
    public class JobScheduler(
            IBackgroundJobClient client
        ) : IJobScheduler
    {
        public void Schedule<T>(Expression<Action<T>> job, DateTime runAt)
        {
            client.Schedule(job, runAt);
        }
    }
}
