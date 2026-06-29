namespace ReminderApp.Infrastructure.ServiceCollectionExtensions
{
    public class AllowAllDashboardAuthorizationFilter : Hangfire.Dashboard.IDashboardAuthorizationFilter
    {
        public bool Authorize(Hangfire.Dashboard.DashboardContext context)
     => true;
    }
}
