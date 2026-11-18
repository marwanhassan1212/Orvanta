# Orvanta – Project Structure

## 1. Solution Structure

Orvanta uses a single ASP.NET Core MVC project organized into logical folders.

```text
Orvanta/
│
├── Orvanta.sln
│
├── Orvanta/
│   ├── Controllers/
│   │
│   ├── Models/
│   │   ├── Entities/
│   │   └── Enums/
│   │
│   ├── Data/
│   │   ├── ApplicationDbContext.cs
│   │   └── Configurations/
│   │
│   ├── Repositories/
│   │   ├── Interfaces/
│   │   └── Implementations/
│   │
│   ├── Services/
│   │   ├── Interfaces/
│   │   └── Implementations/
│   │
│   ├── DTOs/
│   │
│   ├── ViewModels/
│   │
│   ├── Views/
│   │
│   ├── wwwroot/
│   │   ├── css/
│   │   ├── js/
│   │   ├── images/
│   │   └── lib/
│   │
│   ├── Migrations/
│   │
│   ├── Properties/
│   │
│   ├── Program.cs
│   ├── appsettings.json
│   └── appsettings.Development.json
│
├── docs/
│
└── README.md
```

## 2. Folder Responsibilities

| Folder | Responsibility |
|---|---|
| Controllers | Handles HTTP requests and coordinates application operations |
| Models/Entities | Contains business entities |
| Models/Enums | Contains system enumerations |
| Data | Database context and EF Core configurations |
| Repositories/Interfaces | Repository contracts |
| Repositories/Implementations | Database access implementations |
| Services/Interfaces | Service contracts |
| Services/Implementations | Business logic implementations |
| DTOs | Transfers data between application components |
| ViewModels | Provides data required by MVC Views |
| Views | Razor UI pages |
| wwwroot | Static frontend assets |
| Migrations | EF Core database migrations |

## 3. General Development Rules

- Controllers should remain focused on handling requests.
- Business rules should be implemented in Services.
- Repositories should handle data access.
- Database configurations should be separated from entity classes.
- DTOs should be used where data transfer requires them.
- ViewModels should represent the data needed by specific Views.
- Dependency Injection should be used to register application services and repositories.

## 4. Structure Evolution

The project structure may be extended when new requirements arise.

Any significant structural change must be reflected in the architecture documentation.