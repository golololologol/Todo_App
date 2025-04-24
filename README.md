# Todo App

A simple Todo application using a .NET layered architecture with a frontend, a Web API backend, and a DB. Supports Docker-based deployment.


## Architecture

- **Todo.Domain**: Core business entities and interfaces.  
- **Todo.Infrastructure**: Data access implementation using Entity Framework Core and PostgreSQL.  
- **Todo.Application**: Application services, DTOs, and business logic.  
- **Todo.Web.Api**: ASP.NET Core Web API exposing RESTful endpoints.  
- **Todo.Web**: Frontend UI (e.g., React or Razor Pages).  


## Prerequisites

- .NET 6 SDK
- Docker & Docker Compose


## Getting Started

### Run with Docker

```bash
docker-compose up --build
```
- API available at http://localhost:5000  
- UI available at http://localhost:5001  


## API Endpoints

| Method | Route               | Description             |
| ------ | ------------------- | ----------------------- |
| GET    | /api/todos          | List all todos          |
| GET    | /api/todos/{id}     | Get a single todo       |
| POST   | /api/todos          | Create a new todo       |
| PUT    | /api/todos/{id}     | Update an existing todo |
| DELETE | /api/todos/{id}     | Delete a todo           |


## Frontend

- Reads/writes via `WEB_API_URL` env var (default: `http://web-api-todo/`)  
- Implements listing, creation, editing, and deletion of tasks.  
