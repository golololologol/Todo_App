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

### Todo Lists
| Method | Route                  | Description               |
| ------ | ---------------------- | ------------------------- |
| GET    | /api/todolists         | List all todo lists       |
| GET    | /api/todolists/{id}    | Get a single todo list    |
| POST   | /api/todolists         | Create a new todo list    |
| PUT    | /api/todolists/{id}    | Update an existing list   |
| DELETE | /api/todolists/{id}    | Delete a todo list        |

### Todo Tasks
| Method | Route                  | Description                   |
| ------ | ---------------------- | ----------------------------- |
| GET    | /api/todotasks         | List all todo tasks           |
| GET    | /api/todotasks/{id}    | Get a single todo task        |
| POST   | /api/todotasks         | Create a new todo task        |
| PUT    | /api/todotasks/{id}    | Update an existing todo task  |
| DELETE | /api/todotasks/{id}    | Delete a todo task            |

### Users
| Method | Route               | Description               |
| ------ | ------------------- | ------------------------- |
| GET    | /api/users          | List all users            |
| GET    | /api/users/{id}     | Get a single user         |
| POST   | /api/users          | Create a new user         |
| PUT    | /api/users/{id}     | Update an existing user   |
| DELETE | /api/users/{id}     | Delete a user             |

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
