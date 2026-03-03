# Local Event & Workshop Booking Platform

A platform to discover, book, and manage local events and workshops. This repository contains the project documentation and planning; implementation is in progress.

## Features

- Browse events by category, date, or location
- Book and cancel reservations; view booking history
- Organizers can create, edit, and delete events; track attendees; mark events as free or paid
- Admins can approve events, manage users/organizers, and view platform statistics

## Getting started

Recommended: start services with Docker:

1. docker compose up --build -d
   - SQL Server and backend will be started. Backend listens on http://localhost:5000.

Run services individually for development:

- Backend:
  1. cd backend
  2. dotnet run

- Frontend:
  1. cd frontend
  2. npm install
  3. npm run dev

API sample endpoint: GET /api/events (returns sample events from the database).

## Contributing

Contributions are welcome: fork the repo, create a feature branch, and open a pull request. Add or update documentation to help others get started.

## License

Add a LICENSE file to declare the project's license.

---

(Updated README based on project planning in AGENTS.md)
