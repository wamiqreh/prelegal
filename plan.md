Problem

Scaffold a .NET / React project with separate backend and frontend folders, containerized for combined or individual runs.

Requirements from ticket KAN-2

- Frontend: React
- Backend: .NET Core API with SQL Server
- ORM: Entity Framework
- Project should run in a Docker container where React build files are served within the same container
- Project should also run individually (frontend and backend separately)

Proposed approach

1. Repository layout

   backend/        -> .NET 8 (or latest LTS) Web API project
   frontend/       -> React app (Vite or Create React App)
   docker/         -> docker-related files (combined Dockerfile, docker-compose.yml)
   README.md       -> Run instructions

2. Implementation steps (todos)

- scaffold-frontend: Create React app using Vite, add basic Hello World page and a proxy setup for API calls.
- scaffold-backend: Create ASP.NET Core Web API project, add EF Core with SQL Server provider, add a sample entity and migration.
- dockerize-combined: Create multi-stage Dockerfile to build frontend and backend and serve static files from backend (using UseStaticFiles or embedded files).
- docker-compose: Create docker-compose.yml to run SQL Server, backend, and frontend (optional separate service for dev mode).
- docs: Update README with instructions for local dev, running individually, and running via docker-compose.

3. Acceptance criteria

- Running docker-compose up builds and starts SQL Server, backend, and serves the frontend from the backend container.
- Backend exposes a health endpoint and an API endpoint that returns sample data from EF Core.
- Frontend can fetch from backend API and display sample data.
- Projects can be run independently: `dotnet run` in backend and `npm run dev` in frontend.

Notes and decisions

- Use .NET 8 or latest LTS available on dev machines; choose explicit TargetFramework to avoid surprises.
- Use Vite + React for faster dev experience. CRA is acceptable alternative if the user prefers.
- Use SQL Server image (mcr.microsoft.com/mssql/server) for parity with requirements; provide a fallback using SQLite for local dev if user prefers not to run SQL Server.

## Progress

- ✅ Frontend scaffolded with Vite React; dev server confirmed running locally.
- ✅ Backend scaffolded on .NET 8 with Swagger and sample Event endpoints; EF Core model/migration created and InitialCreate migration applied (using SQL Server container).
- ✅ Dockerization: backend Dockerfile added and backend configured to apply migrations at startup.
- ✅ docker-compose: added compose to run SQL Server and backend; services brought up and migration applied successfully.
- ✅ Frontend wired to backend (`/api/events`) via Vite proxy and App.jsx fetch implementation.
- ✅ README updated with docker and local run instructions.

Next steps

- Serve the built frontend from the backend container: update Dockerfile to perform a multi-stage build that builds the frontend and copies the static assets into the backend publish output, then configure the backend to serve static files.
- Add optional frontend Dockerfile and adjust docker-compose for a combined production image (or keep separate dev services for convenience).
- Add health checks, documentation for production deployment, and CI configuration.

