# Auto-Jeremy

An automation platform for streamlining daily technical workloads across multiple domains.

## Project Overview

Auto-Jeremy is designed to automate routine tasks in the following areas:

1. SQL Server Database Development
2. Power BI Development
3. .NET Service Development
4. .NET Web Development
5. Database Administration
6. Architecture Documentation

## System Architecture

![System Architecture](docs/architecture-diagram.png)

### Core Components

- **Orchestration Engine**: Central service for managing and scheduling automation tasks
- **Domain Modules**: Specialized modules for each technical domain
- **Web Dashboard**: User interface for configuration, monitoring, and reporting
- **Storage**: Configuration database and template repository

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- SQL Server 2019+
- Node.js 18+ (for web dashboard)
- Power BI SDK (for Power BI automation)

### Installation

1. Clone the repository
2. Run `dotnet restore` to restore dependencies
3. Configure the connection strings in `appsettings.json`
4. Run `dotnet run` to start the application

## Project Structure

```
Auto-Jeremy/
├── src/
│   ├── AutoJeremy.Core/              # Core orchestration engine
│   ├── AutoJeremy.Web/               # Web dashboard
│   ├── AutoJeremy.SqlServer/         # SQL Server automation module
│   ├── AutoJeremy.PowerBI/           # Power BI automation module
│   ├── AutoJeremy.DotNetService/     # .NET service automation
│   ├── AutoJeremy.DotNetWeb/         # .NET web app automation
│   ├── AutoJeremy.DbAdmin/           # Database administration tools
│   └── AutoJeremy.Documentation/     # Architecture documentation tools
├── tests/                            # Test projects
├── docs/                             # Documentation
└── scripts/                          # Deployment and utility scripts
```

## License

MIT 