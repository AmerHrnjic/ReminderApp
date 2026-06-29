using Microsoft.Extensions.DependencyInjection;
using ReminderApp.Application.Services;
using ReminderApp.Application.Services.Interfaces;

namespace ReminderApp.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services)
        {
            services.AddScoped<IReminderService, ReminderService>();

            return services;
        }
    }
}
