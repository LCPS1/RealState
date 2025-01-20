RealState Application - Property Management System
This repository contains the source code for the RealState Application, a property management system designed to facilitate property and image management, including the creation and updates of property records and their associated images. The system integrates various layers, from the database layer through to the business logic, and provides a range of operations for managing properties and images in a real estate environment.

Project Overview
The RealState application enables users to:

Create, update, and manage properties, including their address and pricing details.
Add, update, and manage property images associated with each property.
Ensure smooth transaction handling with database operations for property creation and image management.
Integrate domain-driven design (DDD) principles for clean architecture and maintainable code.
Key Features
1. Property Management
Create Property: Supports the creation of new property records with details such as address, price, internal code, and owner information.
Update Property: Enables the modification of property details such as price, year of construction, and address information.
Validation: Ensures valid property data is provided before persistence. Invalid names, prices, and years are automatically rejected.
2. Property Image Management
Create Property Image: Facilitates the creation and association of images with specific properties. Images are stored with their file path, description, and status.
Error Handling: Implements error handling for scenarios where the repository or database fails to process an image.
3. Unit of Work and Repository Pattern
The project follows the Unit of Work and Repository patterns, abstracting the database operations and ensuring that multiple changes across different entities are committed or rolled back as a single transaction.
4. Domain-Driven Design (DDD)
The system is structured following the principles of Domain-Driven Design (DDD). Entities such as PropertyBuilding, PropertyImage, and other domain objects are modeled to reflect the real-world business logic and rules.
Use of the Command Handler pattern allows for decoupling the application logic from the user interface layer.
5. Test-Driven Development (TDD)
Comprehensive unit tests are written for each feature, ensuring proper validation and business logic enforcement. The tests cover use cases for both valid and invalid data inputs.
6. Fluent Assertions & NSubstitute
The project uses Fluent Assertions to provide a rich, readable syntax for assertions in tests.
NSubstitute is employed to mock dependencies and isolate the units of work during testing, making the tests faster and more reliable.
Technologies
C# / .NET 6+
FluentAssertions for assertions in unit tests.
NSubstitute for mocking dependencies in tests.
ASP.NET Core for backend APIs.
Unit of Work Pattern for transaction management.
Repository Pattern for abstracting database access.
Entity Framework Core for data access
