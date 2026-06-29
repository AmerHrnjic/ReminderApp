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

### Configure environment variables

Create a `.env` file in the project root.

Example:

```env
EmailSettings__UserName=test@gmail.com
EmailSettings__Password=testpw
```

### Run with Docker(reommended)

```bash
docker compose up --build
```

Or run the Applicatioin locally:

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
ReminderApp.Api/
ReminderApp.Application/
ReminderApp.Domain/
ReminderApp.Infrastructure/
ReminderApp.Worker/
docker-compose.yml
```

## API

| Method | Endpoint | Description |
|---------|----------|-------------|
| GET | /reminders | Get all reminders |
| POST | /reminders | Create reminder |

## Environment Variables

| Variable | Description |
|----------|-------------|
| EmailSettings__UserName | test@gmail.com |
| EmailSettings__Password | testpw |
| EmailSettings__Host | smtp.gmail.com |
| EmailSettings__Port | 587 |
| EmailSettings__FromName | ReminderApp |
| EmailSettings__FromEmail | test@gmail.com |

## License

MIT
