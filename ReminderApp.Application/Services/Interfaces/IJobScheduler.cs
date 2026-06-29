using System.Linq.Expressions;

namespace ReminderApp.Application.Services.Interfaces
{
    public interface IJobScheduler
    {
        void Schedule<T>(Expression<Action<T>> job, DateTime runAt);
    }
}
