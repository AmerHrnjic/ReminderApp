# ReminderApp

A simple reminder management API built with ASP.NET Core and EF Core. Supports creating reminders and sending scheduled email notifications using Hangfire.

## Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- Postgres
- Hangfire
- MailKit
- Docker & Docker Compose

## Features

- Fetch and create reminders
- Schedule background email notifications
- Hangfire dashboard
- Environment-based configuration
- Docker support

## Getting Started

### Clone the repository

```bash
git clone <repository-url>
cd ReminderApp
```
### Run with Docker(recommended)

#### Configure environment variables

Create a `.env` file in the ReminderApp.Worker and ReminderApp.Api project. The examples of .env files are already provided as .env.example files. Remove the .example extension to have a working .env file.

NOTE:
Env files are setup for gmail email and password. Look at 'Environment Variables' section for setup of other mail providers.

This settings below refer to email address that is used to send the reminders via email. For a gmail address to be valid it needs to have 2fa enabled and a App password generated.

Example:

```env
EmailSettings__UserName=test@gmail.com
EmailSettings__Password=testpw
```

```bash
docker compose up --build
```

###  Or run the Applicatioin locally:

Before running the app create a postgres docker container:
```
 docker run -d --name reminders-postgres -e POSTGRES_DB=reminders -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=postgres -p 5432:5432 -v pgdata:/var/lib/postgresql/data postgres:16-alpine
```

Change the Connection string and environment varaibles in appsettings.json

Run the two projects using:
```bash
dotnet run --project src/ReminderApp.Api
dotnet run --project src/ReminderApp.Worker
```

## Project Structure

```
ReminderApp.Api/             # ASP.NET Core Web API (controllers, middleware, DI)
ReminderApp.Application/     # Business logic, services, interfaces
ReminderApp.Common/          # DTOs, Enums, Validation attributes
ReminderApp.Domain/          # Core domain models, entities, and business rules
ReminderApp.Infrastructure/  # EF Core, repositories, external services
ReminderApp.Worker/          # Scheduled email processing using hangfire server.
docker-compose.yml           # Runs the API, Worker, and SQL Server together
```
The solution has two main service:
  - ReminderApp.Api. Handles HTTP requests and exposes the REST API endpoints to clients, Additionally it schedules hangfire jobs when reminder is created.
  - ReminderApp.Worker. Polls the data using Hangfire servers and executes jobs at their scheduled time.

Aside from these two services following class librares were implemented:
  - ReminderApp.Application - Coordinates multiple services to execute Buissnes logic. Provides interfaces of needed services to other projects (example: ReminderRepository). Serves as the centar point of the solution.
  - ReminderApp.Common - Contains the code that any project might use such as:  DTOs, Enums, Validation attributes
  - ReminderApp.Domain - Contains Entities and buisness rules over them. So far, the only rule is the method to change the status of the Reminder object.
  - ReminderApp.Infrastrcture - Responsible for EFCore and all the external services (Hangfire, MailKit). Contains ServiceCollectionExtensions for ReminderApp.Api and ReminderApp.Worker so the DI of these services is logically separated.

The implementation was done this way to separate the responisibilites of different parts of the system and decouple them.
This structure avoids circular dependency.

The original idea was to us SqLite as the database, but Potgres was used in the end. There were 3 main reasons for this:
  - There are 2 services that use the datbase ( Worker and Api). This means they both have to have access to the db. Sqlite is in essence a file, this means creating a shared volume between the services and then creating the db file inside it. This defeats the purpose of Sqlite, since the idea of using it was to keep the solution simple and this made containerization and orchestration much more of a hassle.
  - Hangfire support for SqLite is not too great. There are a couple of loosely maintained community packages. Postgres has a regularly maintained package.
  - Any further scaling with SqLite would be complicated.
    
## API

| Method | Endpoint | Description |
|---------|----------|-------------|
| GET | /reminders | Get all reminders |
| POST | /reminders | Create a reminder |

## Environment Variables

Worker
| Variable | Description |
|----------|-------------|
| EmailSettings__UserName | test@gmail.com |
| EmailSettings__Password | testpw |
| EmailSettings__Host | smtp.gmail.com |
| EmailSettings__Port | 587 |
| EmailSettings__FromName | ReminderApp |
| EmailSettings__FromEmail | test@gmail.com |

Api
| Variable | Description |
|----------|-------------|
| ASPNETCORE_ENVIRONMENT | Development/Production |

Use Development to enable swagger ui and hangfire dashboards. Production disables these.
## License

MIT
