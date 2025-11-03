# 🧱 Vertical Slice Architecture in C# (.NET)

The **Vertical Slice Architecture** organizes code by **features or functionalities**, not by technical layers.
Each slice or module is **self-contained**, including its own workflow (endpoint, commands, queries, business logic, and models) and communicates with the infrastructure through interfaces.

---

## 🗂️ General Project Structure

```md
MyApp/
│
├── src/
│   ├── MyApp.Api/                  # Entry layer: configuration and app startup
│   │   ├── Program.cs
│   │   ├── appsettings.json
│   │   ├── DependencyInjection.cs
│   │   └── Extensions/
│   │       └── EndpointExtensions.cs
│   │
│   ├── MyApp.Features/             # Main folder containing "slices" or modules
│   │   ├── Users/
│   │   │   ├── CreateUser/
│   │   │   │   ├── CreateUserCommand.cs
│   │   │   │   ├── CreateUserHandler.cs
│   │   │   │   ├── CreateUserValidator.cs
│   │   │   │   └── CreateUserEndpoint.cs
│   │   │   │
│   │   │   ├── GetUser/
│   │   │   │   ├── GetUserQuery.cs
│   │   │   │   ├── GetUserHandler.cs
│   │   │   │   └── GetUserEndpoint.cs
│   │   │   │
│   │   │   ├── Models/
│   │   │   │   ├── User.cs
│   │   │   │   └── UserDto.cs
│   │   │   │
│   │   │   └── IUserRepository.cs
│   │   │
│   │   ├── Orders/
│   │   └── Products/
│   │
│   └── MyApp.Infrastructure/
│       ├── Persistence/
│       │   ├── AppDbContext.cs
│       │   ├── Configurations/
│       │   └── Migrations/
│       ├── Repositories/
│       │   └── UserRepository.cs
│       └── DependencyInjection.cs
│
└── tests/
    └── MyApp.Tests/
```

---

## 🧩 Layers and Responsibilities

### 🧭 `MyApp.Api/`

This is the **entry point** of the project.
It is responsible for:

* Configuring the application (`Program.cs`).
* Registering dependencies (`DependencyInjection.cs`).
* Mapping all endpoints from each slice.
* Setting up middleware, logging, and CORS.

**Example: `Program.cs`**

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(); // Registers Feature services
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.MapEndpoints(); // Maps all endpoints from Features

app.Run();
```

---

### 🧠 `MyApp.Features/`

Contains **all the system’s features**.
Each subfolder (e.g., `Users`, `Orders`, `Products`) represents a **functional module**.
Within each module, every use case (e.g., `CreateUser`, `GetUser`) has its own set of files.

#### 📦 Internal structure of a slice (`Users/CreateUser/`)

| File                       | Responsibility                                          |
| -------------------------- | ------------------------------------------------------- |
| **CreateUserCommand.cs**   | Defines the input data for the use case.                |
| **CreateUserHandler.cs**   | Contains business logic (implements `IRequestHandler`). |
| **CreateUserValidator.cs** | Validates input using FluentValidation.                 |
| **CreateUserEndpoint.cs**  | Defines the HTTP endpoint (Minimal API or Controller).  |

---

### 🧱 `MyApp.Infrastructure/`

Contains the **infrastructure layer**:

* Database (EF Core, Dapper, etc.)
* Repository implementations.
* External API integrations.
* Dependency injection configuration.

**Example: `UserRepository.cs`**

```csharp
public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}
```

---

## ⚙️ Main Files Inside a Slice

### 🧩 1. `CreateUserCommand.cs`

Represents the **command** object containing the request data.

```csharp
public record CreateUserCommand(string Email, string Password) : IRequest<Guid>;
```

---

### ⚙️ 2. `CreateUserHandler.cs`

Contains the **business logic** for the use case.

```csharp
public class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _repository;

    public CreateUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(request.Email, request.Password);
        await _repository.AddAsync(user);
        return user.Id;
    }
}
```

---

### ✅ 3. `CreateUserValidator.cs`

Validates input data using **FluentValidation**.

```csharp
public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).MinimumLength(6);
    }
}
```

---

### 🌐 4. `CreateUserEndpoint.cs`

Defines the **HTTP endpoint** (using Minimal APIs or Controllers).

```csharp
public static class CreateUserEndpoint
{
    public static void MapCreateUserEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/users", async (CreateUserCommand command, IMediator mediator) =>
        {
            var id = await mediator.Send(command);
            return Results.Created($"/users/{id}", new { id });
        });
    }
}
```

---

### 🧩 5. `User.cs` (Domain Entity)

Defines the structure of the **business model**.

```csharp
public class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Email { get; private set; }
    public string Password { get; private set; }

    public User(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
```

---

### 🗃️ 6. `IUserRepository.cs`

Defines the **interface** abstracting data access.

```csharp
public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User?> GetByIdAsync(Guid id);
}
```

---

## 🧠 General Request Flow

```md
(HTTP Request)
     ↓
 CreateUserEndpoint
     ↓
 CreateUserCommand
     ↓
 CreateUserHandler
     ↓
 IUserRepository → UserRepository
     ↓
 AppDbContext (EF Core)
     ↓
 (Database)
```

---

## 🧰 Recommended Packages

| Purpose              | NuGet Package                             |
| -------------------- | ----------------------------------------- |
| Mediator / CQRS      | `MediatR`                                 |
| Validation           | `FluentValidation`                        |
| ORM                  | `Microsoft.EntityFrameworkCore`           |
| Database Provider    | `Microsoft.EntityFrameworkCore.SqlServer` |
| Dependency Injection | Built into `.NET`                         |
| Minimal APIs         | Built into `.NET 9`                       |

---

## ✅ Benefits of the Slice Approach

| Advantage                      | Description                                                   |
| ------------------------------ | ------------------------------------------------------------- |
| **High Cohesion**              | Each module contains everything needed for its functionality. |
| **Low Coupling**               | Modules are independent from each other.                      |
| **Scalable**                   | Easy to extend or modify without breaking others.             |
| **Perfect for CQRS + MediatR** | Commands and queries are naturally organized.                 |
| **Simpler Testing**            | Each slice can be tested in isolation.                        |

---

## 🧩 Final Summary

**Vertical Slice Architecture = Modularity + Maintainability + Clarity.**

Each feature has its own **complete vertical flow**:

> Endpoint → Command/Query → Handler → Repository → Persistence

This approach prevents large monolithic layers and improves **developer productivity** and **code maintainability**.
