@ -1,2 +1,199 @@
# TestLetShare
Technical Challenge – Authentication Module in .NET 8

## Project Overview

TestLetShare is a .NET 8 Web API project that implements a complete authentication system with JWT tokens. The project follows Clean Architecture principles and includes user management functionality.

## Architecture

The project is structured using Clean Architecture with the following layers:

- **TestLetshare.API**: Web API layer with controllers and middleware
- **TestLetshare.Application**: Business logic layer with commands, DTOs, and services
- **TestLetshare.Domain**: Domain entities and business rules
- **TestLetshare.Infrastructure**: Data access layer with Entity Framework Core and PostgreSQL

## Features

- JWT-based authentication
- User management
- PostgreSQL database integration
- FluentValidation for request validation
- Swagger/OpenAPI documentation
- CORS policy configuration
- Exception handling middleware

## Prerequisites

- .NET 8 SDK
- PostgreSQL database
- Visual Studio 2022, VS Code, or any .NET-compatible IDE

## Getting Started

### 1. Database Setup

1. Install PostgreSQL on your system
2. Create a new database named `postgres` (or update the connection string in `appsettings.json`)
3. Update the connection string in `TestLetshare/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=12345"
}
```

### 2. Running the Application

1. **Clone the repository**:
   ```bash
   git clone <repository-url>
   cd TestLetShare
   ```

2. **Restore dependencies**:
   ```bash
   dotnet restore
   ```

3. **Run database migrations** (if using Entity Framework migrations):
   ```bash
   cd TestLetshare
   dotnet ef database update
   ```

4. **Start the application**:
   ```bash
   dotnet run
   ```

5. **Access the application**:
   - API: `https://localhost:7001` or `http://localhost:5001`
   - Swagger UI: `https://localhost:7001/swagger` or `http://localhost:5001/swagger`

## API Endpoints

### Authentication

- **POST** `/api/auth/login`
  - Authenticates a user and returns JWT tokens
  - Request body: `SignInCommand` with username and password
  - Returns: JWT access token and refresh token

### User Management

- **GET** `/api/user/GetUsers`
  - Retrieves all users (requires authentication)
  - Headers: `Authorization: Bearer <jwt_token>`

## Testing the API

### 1. Using Swagger UI

1. Navigate to the Swagger UI at `/swagger`
2. Use the "Try it out" feature to test endpoints
3. For protected endpoints, click "Authorize" and enter your JWT token

### 2. Using curl

**Login Example**:
```bash
curl -X POST "https://localhost:7001/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "username": "your_username",
    "password": "your_password"
  }'
```

**Get Users Example** (with authentication):
```bash
curl -X GET "https://localhost:7001/api/user/GetUsers" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN"
```

### 3. Using Postman

1. Import the API endpoints
2. For authentication, send a POST request to `/api/auth/login`
3. Copy the JWT token from the response
4. Add the token to the Authorization header for subsequent requests

## Configuration

### JWT Settings

Update JWT configuration in `appsettings.json`:

```json
"Jwt": {
  "Secret": "your-secret-key",
  "Issuer": "AuthAPI",
  "Audience": "AuthClient",
  "AccessTokenExpirationMinutes": 60,
  "RefreshTokenExpirationMinutes": 1440
}
```

### CORS Policy

CORS is configured to allow requests from any origin in development. Update the CORS policy in `CorsExtensions.cs` for production.

## Project Structure

```
TestLetShare/
├── TestLetshare/                    # API Layer
│   ├── Controllers/                 # API Controllers
│   ├── Extensions/                  # Service Extensions
│   ├── Middlewares/                 # Custom Middleware
│   └── Program.cs                   # Application Entry Point
├── TestLetshare.Application/        # Application Layer
│   ├── Features/                    # Feature-based Organization
│   │   ├── Auth/                    # Authentication Features
│   │   └── User/                    # User Management Features
│   └── Common/                      # Shared Models and Interfaces
├── TestLetshare.Domain/             # Domain Layer
│   └── Entities/                    # Domain Entities
└── TestLetshare.Infrastructure/     # Infrastructure Layer
    ├── Data/                        # Database Context
    ├── Repositories/                # Data Access
    └── Services/                    # External Services
```

## Development

### Adding New Features

1. Create commands and DTOs in the Application layer
2. Implement services in the Infrastructure layer
3. Add controllers in the API layer
4. Update Swagger documentation

### Validation

The project uses FluentValidation for request validation. Add validators in the `Validators` folder within each feature.

## Troubleshooting

### Common Issues

1. **Database Connection**: Ensure PostgreSQL is running and the connection string is correct
2. **JWT Token**: Verify the JWT secret is properly configured
3. **CORS**: Check CORS policy settings if making requests from a frontend application

### Logs

Check the console output for detailed error messages and logs.

## Contributing

1. Follow the existing code structure and naming conventions
2. Add appropriate validation and error handling
3. Update documentation for new features
4. Test all endpoints before submitting changes

## License

This project is part of a technical challenge and is for demonstration purposes.
