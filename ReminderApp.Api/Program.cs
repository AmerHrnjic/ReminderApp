using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReminderApp.Api;
using ReminderApp.Application;
using ReminderApp.Infrastructure;
using ReminderApp.Infrastructure.ServiceCollectionExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApiInfrastructure(builder.Configuration);
builder.Services.AddApplicationLayerServices();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var messages = context.ModelState
            .Where(x => x.Value?.Errors.Count > 0)
            .SelectMany(x => x.Value!.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();

        return new BadRequestObjectResult(messages);
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    Console.WriteLine(System.Net.Dns.GetHostName());

    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHangfireDashboard("/hangfire", new DashboardOptions
    {
        Authorization = new[]
        {
            new AllowAllDashboardAuthorizationFilter()
        }
    });

}
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ReminderAppDbContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
