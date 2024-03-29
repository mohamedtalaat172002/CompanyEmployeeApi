
# CompanyEmployee ASP.NET Web API Application

## Overview
CompanyEmployee is an ASP.NET Web API application designed to manage employee data for a company. It follows the Onion architecture pattern, providing a modular and maintainable structure.
## Status
This project is currently being developed and maintained. New features and improvements are regularly added.
### Reference:
This project is based on the principles and practices outlined in the book "Ultimate.ASP.NET.Core.Web.API", which serves as a guide for implementing Onion Architecture with ASP.NET.
## Technologies Used
- ASP.NET API application
- Entity Framework 
- SQL Server 
### Description:
This project follows the Onion Architecture pattern, which organizes the codebase into distinct layers:

- **Domain Layer**: Contains the core domain models, business logic, and domain services. This layer defines the fundamental building blocks of the application's domain.

- **Service Layer**: Implements the application's business logic and orchestrates interactions between the domain layer and other layers. It encapsulates use cases and exposes them to the presentation layer.

- **Presentation Layer**: Includes the user interface components, such as controllers, views, and API endpoints. This layer interacts with users and external systems, handling input and output.

- **Infrastructure Layer**: Provides implementations for external dependencies and infrastructure concerns, such as data access, logging, and external services integration. It includes database access, repository implementations, external API clients, and other infrastructure-related components.
### implemented-till now-:

- **Logger**: Implements logging functionality to trace application events and errors, aiding in debugging and monitoring.
  
- **Repository Pattern**: Utilizes the repository pattern to abstract data access logic, promoting separation of concerns and testability.
  
- **DTO Classes**: Defines Data Transfer Object (DTO) classes to transfer data between layers and external systems, ensuring data encapsulation and reducing coupling.
 
- **AutoMapper**: Utilizes AutoMapper library for object-to-object mapping, simplifying the mapping between domain models and DTOs.

- **Global Error Handling**: Manages application-wide error handling.

- **Invalid Request Handling**: Manages and responds to invalid requests effectively, handling the exception in a better way than throwing try and catch everywhere in the project.


- **CRUD Operations**: Enables Create, Read, Update, and Delete operations for efficient management of employees and companies data.
   
    Collection of Companies: Users can add a collection of companies in one request
   
    Company with Employees: Allows the creation of a company entity with its associated employees in a single request, reducing the number of API calls required.
   
    Also the patch end point is added.

- **validation**: implements validation mechanisms to ensure data integrity and correctness.

- **Asynchronous code**: Employs asynchronous programming techniques for operations to improve performance and responsiveness.

## Contact Information
For questions, mail [Mohamed Talaat](mailto:mohamedtalaat172002@gmail.com).
