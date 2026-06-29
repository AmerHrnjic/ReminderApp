using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReminderApp.Application.Jobs;
using ReminderApp.Application.Repositories.Interfaces;
using ReminderApp.Application.Services.Interfaces;
using ReminderApp.Infrastructure.Repositories.Implementations;
using ReminderApp.Infrastructure.Services;

namespace ReminderApp.Infrastructure.ServiceCollectionExtensions
{
    public static class WorkerServiceCollectionExtensions
    {
        public static IServiceCollection AddWorkerInfrastructure(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddDbContext<ReminderAppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Postgres")));

            services.AddScoped<IReminderRepository, ReminderRepository>();

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IReminderJob, ReminderJob>();
            services.Configure<EmailSettings>(
                configuration.GetSection("EmailSettings"));

            var conn = configuration.GetConnectionString("Postgres");

            services.AddHangfire(config =>
                 config.UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(c => c.UseNpgsqlConnection(conn)));

            services.AddHangfireServer();

            return services;
        }
    }
}
