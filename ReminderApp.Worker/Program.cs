using ReminderApp.Infrastructure.ServiceCollectionExtensions;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddWorkerInfrastructure(builder.Configuration);

var host = builder.Build();
host.Run();
