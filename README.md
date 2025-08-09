🧾 PersonRegistry
 ----------------------
PersonRegistry is a modular, maintainable ASP.NET Core Web API.

This project demonstrates:

 ✅ Domain-Driven Design (DDD) for rich domain modeling and business logic encapsulation
 
 ✅ Clean Architecture to separate concerns across layers
 
 ✅ CQRS (Command Query Responsibility Segregation) using MediatR for handling commands and queries
 
 ✅ Entity Framework Core (EF Core) for database access
 
 ✅ Dockerized infrastructure for easy deployment

 ✅Auditing: Automatic CreatedAtUtc and LastModifiedAtUtc

 ✅Soft Delete: Global query filter hides deleted entities by default

 ✅Filtered Unique Indexes: Enforce uniqueness for active (non-deleted) records

This solution serves as a practical reference for building real-world, enterprise-grade application architecture in .NET.




---------------------------------------------------------------------------------------------------------
🔧 Setup & Run with Docker
  Clone the repository:
  ```powershell
  cd PersonRegistry
  docker compose up --build
  ```

ASP.NET Core API at http://localhost:8080
SQL Server at localhost:1433

------------------------------------------------------------------------------------------------
👤 Person Endpoints
| Method   | Endpoint             | Description                |
| -------- | -------------------- | -------------------------- |
| `POST`   | `/api/person`        | Create new person          |
| `PUT`    | `/api/person`        | Update person's basic info |
| `DELETE` | `/api/person/{id}`   | Delete a person by ID      |
| `GET`    | `/api/person/{id}`   | Get person by ID           |
| `GET`    | `/api/person/search` | Search persons by filters  |

🔗 Relation Endpoints

| Method   | Endpoint                     | Description       |
| -------- | ---------------------------- | ----------------- |
| `POST`   | `/api/person/relations`      | Add a relation    |
| `DELETE` | `/api/person/{id}/relations` | Remove a relation |

📞 Phone Number Endpoints

| Method   | Endpoint                        | Description           |
| -------- | ------------------------------- | --------------------- |
| `POST`   | `/api/person/phonenumbers`      | Add a phone number    |
| `DELETE` | `/api/person/{id}/phonenumbers` | Remove a phone number |
