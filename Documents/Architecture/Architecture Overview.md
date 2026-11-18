# Orvanta – Architecture Overview

## 1. Architecture Style

Orvanta follows a **Layered Monolithic Architecture** implemented within a single ASP.NET Core MVC project.

The application is organized into logical folders, with each folder responsible for a specific part of the system.

This approach keeps development straightforward while maintaining separation of responsibilities and clear code organization.

## 2. Application Layers

The application is organized into the following logical layers:

### 2.1 Presentation Layer
Responsible for handling user interactions and displaying application data.

Includes:
- MVC Controllers
- Razor Views
- ViewModels
- Frontend assets (HTML, CSS, JavaScript)

### 2.2 Business Logic Layer
Responsible for implementing application operations and business rules.

Includes:
- Services
- Service Interfaces
- DTOs

### 2.3 Data Access Layer
Responsible for communicating with the database and retrieving or persisting data.

Includes:
- Repositories
- Repository Interfaces
- Unit of Work
- Entity Framework Core
- Database Context
- Entity Configurations

### 2.4 Domain Model
Contains the core business entities and their fundamental properties and relationships.

Includes:
- Entities
- Enums
- Domain-related definitions

## 3. Technology Stack

- ASP.NET Core MVC
- C#
- Entity Framework Core
- SQL Server
- LINQ
- ASP.NET Core Identity
- Razor Views
- HTML, CSS, JavaScript

## 4. Architectural Principles

The application follows these principles:

- Separation of Concerns
- Single Responsibility Principle
- Dependency Injection
- Repository Pattern
- Unit of Work Pattern
- Service Layer Pattern
- Interface-Based Abstraction
- Data Integrity
- Secure Authorization

## 5. Dependency Flow

The logical dependency flow is:

Presentation → Business Logic → Data Access

The Domain Model is used by the other layers as the central representation of business entities.

## 6. Architectural Scope

Orvanta is developed as a single deployable application.

The MVP does not use:
- Microservices
- CQRS
- MediatR
- Distributed Architecture
- Separate Application Class Libraries

These technologies are outside the current implementation scope.

## 7. Architecture Objective

The objective is to build a maintainable, organized, and scalable business management application without introducing unnecessary architectural complexity.