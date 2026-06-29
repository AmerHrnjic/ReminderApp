using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReminderApp.Application.Repositories.Interfaces;
using ReminderApp.Application.Services.Interfaces;
using ReminderApp.Infrastructure.Repositories.Implementations;
using ReminderApp.Infrastructure.Services;

namespace ReminderApp.Infrastructure.ServiceCollectionExtensions
{
    public static class ApiServiceCollectionExtensions
    {
        public static IServiceCollection AddApiInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            services.AddDbContext<ReminderAppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Postgres")));

            services.AddScoped<IReminderRepository, ReminderRepository>();
            services.AddScoped<IJobScheduler, JobScheduler>();

            var conn = configuration.GetConnectionString("Postgres");

            services.AddHangfire(config =>
                config.UsePostgreSqlStorage(c => c.UseNpgsqlConnection(conn)));


            return services;
        }
    }
}
