# RealState Application - Property Management System

This repository contains the source code for **RealState**, a Property Management System designed to facilitate the creation and management of property records and associated images. The system integrates various layers, from the database layer through to the business logic, providing a range of operations for managing properties, images, and transactions in a real estate environment.

## Project Overview

The **RealState** application enables users to:

- Create, update, and manage properties, including address and pricing details.
- Add, update, and manage property images associated with each property.
- Handle transactions smoothly with database operations for property creation and image management.
- Integrate Domain-Driven Design (DDD) principles to ensure a clean architecture and maintainable code.

## Key Features

### 1. Property Management
- **Create Property**: Supports the creation of new property records with details such as address, price, internal code, and owner information.
- **Update Property**: Enables the modification of property details such as price, year of construction, and address.
- **Validation**: Ensures that valid property data is provided before persistence. Invalid names, prices, and years are automatically rejected.

### 2. Property Image Management
- **Create Property Image**: Facilitates the creation and association of images with specific properties. Images are stored with metadata such as file path, description, and status.
- **Error Handling**: Implements robust error handling for scenarios where the repository or database fails to process an image.

### 3. Unit of Work and Repository Pattern
- The project follows the **Unit of Work** and **Repository** patterns, abstracting database operations. This ensures that multiple changes across different entities are committed or rolled back as a single transaction.

### 4. Domain-Driven Design (DDD)
- The system is structured using **Domain-Driven Design (DDD)** principles, ensuring that entities such as `PropertyBuilding`, `PropertyImage`, and other domain objects reflect real-world business logic and rules.
- The use of the **Command Handler** pattern decouples application logic from the user interface layer.

### 5. Test-Driven Development (TDD)
- Comprehensive unit tests are written for each feature, ensuring proper validation and business logic enforcement. The tests cover use cases for both valid and invalid data inputs.

### 6. Fluent Assertions & NSubstitute
- **Fluent Assertions**: Provides a rich, readable syntax for assertions in unit tests, making them more intuitive and maintainable.
- **NSubstitute**: Employed for mocking dependencies, isolating units of work during testing, and improving test speed and reliability.

## Technologies Used
- **C# / .NET 6+**: Core language and framework for backend development.
- **FluentAssertions**: Library for readable assertions in unit tests.
- **NSubstitute**: Framework for mocking dependencies in unit tests.
- **ASP.NET Core**: For building the backend APIs.
- **Unit of Work Pattern**: Manages transactions across different entities.
- **Repository Pattern**: Abstracts database access for cleaner and maintainable code.
- **Entity Framework Core**: ORM for interacting with the database.

## How to Run the Application

### 1. **Set up the Database with Docker**

This project uses **Docker Compose** for running the SQL Server database container. To set up the environment, use the following steps:

1. **Clone the repository**:
   ```bash
   git clone https://github.com/LCPS1/RealState
   cd RealState

2. **Build and start Docker containersy**:
    docker-compose up --build
This command will:

Set up the SQL Server database using the image mcr.microsoft.com/mssql/server:2019-latest.
Start the application API using the .NET container.

3. **Verify database connection: The database will be running with the following connection string**:

Server=db;Database=RealStateDB;User=sa;Password=realState123;

4. **Database Setup and Migrations**: 

dotnet ef database update -p ./src/RealState.Infrastructure/ -s ./src/RealState.Api/

This will apply any pending migrations to the database.

Note: You can modify the connection string locally by updating the appsettings.json file, but it's recommended to use Docker for development as it isolates the environment.

Alternatively you can add a mifaito to the solotuion and then ush your change sto your local DB 

dotnet ef migrations add Init -p .\src\RealState.Infrastructure\ -s .\src\RealState.Api\      ´


4. **Run API**: 

Once Docker containers are up and running, you can access the RealState API on http://localhost:5001.

To test the API, use any HTTP client (e.g., Postman) to send requests to the available endpoints.

Otherwise use https://localhost:5001 to run t locally using the profiles of .net in launch settings Json.


5. **: Authentication and Authorization**
The application includes mock authentication endpoints for testing purposes:

Register: POST to /api/auth/register
Login: POST to /api/auth/login
These endpoints simulate an Active Directory (AD) authentication system and return a token that you can use for subsequent requests.

Use the token to authenticate requests to the API by including it in the Authorization header

Authorization: Bearer <Your_Token>


6. **. Test in Visual Studio Code**

For ease of development, you can run and debug the application directly from Visual Studio Code.

Ensure that you have the Docker extension installed for VS Code to manage containers directly from the IDE.