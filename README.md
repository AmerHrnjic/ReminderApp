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

- Create, update, and delete reminders
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
Env files are setup for gmail email and password. Look at  'Environment Variables' section for setup of other mail providers.

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
