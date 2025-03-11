# Auto-Jeremy Technical Specification

## 1. System Architecture

### 1.1 Overview

Auto-Jeremy is designed as a modular, extensible automation platform with a microservices-based architecture. The system consists of:

- Core orchestration engine
- Domain-specific modules
- Web dashboard
- Storage services

### 1.2 Architecture Diagram

```
┌─────────────────────────────────────────────────────────────────┐
│                        Web Dashboard                            │
└───────────────────────────────┬─────────────────────────────────┘
                                │
                                ▼
┌─────────────────────────────────────────────────────────────────┐
│                    API Gateway / Authentication                  │
└───────┬───────────┬───────────┬───────────┬───────────┬─────────┘
        │           │           │           │           │
        ▼           ▼           ▼           ▼           ▼           ▼
┌───────────┐ ┌───────────┐ ┌───────────┐ ┌───────────┐ ┌───────────┐ ┌───────────┐
│ SQL Server│ │ Power BI  │ │.NET Service│ │.NET Web   │ │  DB Admin │ │Architecture│
│  Module   │ │  Module   │ │  Module    │ │  Module   │ │  Module   │ │    Docs    │
└───────────┘ └───────────┘ └───────────┘ └───────────┘ └───────────┘ └───────────┘
        │           │           │           │           │           │
        └───────────┴───────────┼───────────┴───────────┴───────────┘
                                │
                                ▼
┌─────────────────────────────────────────────────────────────────┐
│                    Orchestration Engine                          │
└───────────────────────────────┬─────────────────────────────────┘
                                │
                                ▼
┌───────────────────┐ ┌─────────────────────┐ ┌───────────────────┐
│  Configuration DB │ │ Template Repository  │ │   Logging Store   │
└───────────────────┘ └─────────────────────┘ └───────────────────┘
```

## 2. Core Components

### 2.1 Orchestration Engine

The orchestration engine is the central component responsible for:

- Task scheduling and execution
- Module coordination
- State management
- Error handling and recovery

**Technology Stack:**
- .NET 8 Worker Service
- Quartz.NET for scheduling
- MediatR for in-process messaging
- Polly for resilience and retry policies

**Key Interfaces:**
```csharp
public interface ITaskOrchestrator
{
    Task<Guid> ScheduleTask(TaskDefinition taskDefinition, CancellationToken cancellationToken);
    Task<TaskStatus> GetTaskStatus(Guid taskId, CancellationToken cancellationToken);
    Task CancelTask(Guid taskId, CancellationToken cancellationToken);
}

public interface ITaskExecutor
{
    Task<TaskResult> ExecuteTask(TaskDefinition taskDefinition, CancellationToken cancellationToken);
}
```

### 2.2 Module System

Each domain-specific module implements a standard interface:

```csharp
public interface IAutomationModule
{
    string ModuleName { get; }
    IEnumerable<TaskTemplate> GetAvailableTasks();
    Task<bool> ValidateTaskParameters(string taskType, Dictionary<string, object> parameters);
    Task<TaskResult> ExecuteTask(string taskType, Dictionary<string, object> parameters, CancellationToken cancellationToken);
}
```

**Module Registration:**
Modules are discovered and loaded dynamically using MEF (Managed Extensibility Framework).

### 2.3 Web Dashboard

The web dashboard provides a user interface for:

- Task configuration and scheduling
- Monitoring task execution
- Viewing logs and results
- Managing templates and configurations

**Technology Stack:**
- ASP.NET Core 8 MVC/Razor Pages
- Blazor for interactive components
- SignalR for real-time updates
- Bootstrap 5 for UI components

### 2.4 Storage Services

#### 2.4.1 Configuration Database

Stores system configuration, task definitions, and scheduling information.

**Schema (Simplified):**
```
Tasks
- TaskId (PK)
- TaskName
- ModuleName
- TaskType
- Parameters (JSON)
- Schedule (CRON expression)
- IsEnabled

TaskHistory
- ExecutionId (PK)
- TaskId (FK)
- StartTime
- EndTime
- Status
- Result (JSON)
- Logs
```

**Technology:** SQL Server with Entity Framework Core

#### 2.4.2 Template Repository

Stores templates for various automation tasks.

**Structure:**
- Templates organized by module and task type
- Version control for templates
- Support for parameterization

**Technology:** File-based storage with Git integration

## 3. Domain-Specific Modules

### 3.1 SQL Server Database Development Module

**Key Features:**
- Script generation
- Schema comparison
- Query optimization
- Deployment automation

**Technology Stack:**
- SQL Server Management Objects (SMO)
- DacFx for schema comparison
- T-SQL parsing and analysis
- DbUp for migrations

### 3.2 Power BI Development Module

**Key Features:**
- Dataset management
- Report automation
- Deployment pipeline

**Technology Stack:**
- Power BI REST API
- Power BI .NET SDK
- Azure AD authentication

### 3.3 .NET Service Development Module

**Key Features:**
- Service scaffolding
- API endpoint generation
- Test automation

**Technology Stack:**
- Roslyn for code generation
- MSBuild for project manipulation
- NSwag for API client generation

### 3.4 .NET Web Development Module

**Key Features:**
- Web app scaffolding
- UI component generation
- Frontend integration

**Technology Stack:**
- ASP.NET Core templates
- JavaScript/TypeScript tooling
- Webpack/npm integration

### 3.5 Database Administration Module

**Key Features:**
- Performance monitoring
- Index management
- Backup automation

**Technology Stack:**
- DMV queries
- SQL Agent integration
- Performance counter monitoring

### 3.6 Architecture Documentation Module

**Key Features:**
- Diagram generation
- Documentation management
- Code-to-docs synchronization

**Technology Stack:**
- PlantUML/Mermaid integration
- Markdown processing
- Documentation site generation

## 4. Security Considerations

### 4.1 Authentication and Authorization

- Azure AD integration for authentication
- Role-based access control
- Fine-grained permissions for task execution

### 4.2 Secure Storage

- Encryption for sensitive configuration data
- Secure credential management
- Audit logging for all operations

### 4.3 Network Security

- HTTPS for all communications
- API key authentication for service-to-service communication
- IP restrictions for administrative access

## 5. Deployment Model

### 5.1 Development Environment

- Docker containers for local development
- LocalDB for database
- Mock services for external dependencies

### 5.2 Production Environment

- Azure App Service for web components
- Azure SQL Database for storage
- Azure Key Vault for secrets
- Azure DevOps for CI/CD

## 6. Monitoring and Logging

### 6.1 Application Insights

- Performance monitoring
- Exception tracking
- Usage analytics

### 6.2 Structured Logging

- Serilog for structured logging
- Log aggregation in Elasticsearch
- Kibana dashboards for visualization

## 7. Extensibility

### 7.1 Plugin System

- Custom module development
- Third-party integration
- Template customization

### 7.2 API Access

- REST API for programmatic access
- Webhook integration
- Event-driven architecture 