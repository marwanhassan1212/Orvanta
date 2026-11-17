# Non-Functional Requirements – Orvanta

## 1. Introduction

This document defines the non-functional requirements of Orvanta.

These requirements describe the expected quality attributes, operational constraints, security standards, and performance expectations of the system.

## 2. Security Requirements

### NFR-SEC-01 – Authentication Security
The system shall use ASP.NET Core Identity for user authentication and password management.

### NFR-SEC-02 – Password Protection
User passwords shall be stored using secure password hashing mechanisms provided by ASP.NET Core Identity.

### NFR-SEC-03 – Authorization
The system shall enforce role-based authorization on protected pages and business operations.

### NFR-SEC-04 – Session Protection
The system shall protect authenticated sessions and prevent unauthorized access to protected resources.

### NFR-SEC-05 – Input Validation
The system shall validate user input on the server side before processing business operations.

### NFR-SEC-06 – Anti-Forgery Protection
The system shall protect state-changing web requests against Cross-Site Request Forgery (CSRF) attacks.

### NFR-SEC-07 – Sensitive Data Protection
The system shall avoid exposing passwords, credentials, connection strings, and other sensitive information.

### NFR-SEC-08 – Business Operation Authorization
The system shall verify user authorization before executing sensitive operations, including invoice approval, receipt voiding, and supplier payment voiding.

## 3. Performance Requirements

### NFR-PER-01 – Asynchronous Database Operations
The system shall use asynchronous database operations where supported by Entity Framework Core.

### NFR-PER-02 – Pagination
Large data lists shall support server-side pagination.

### NFR-PER-03 – Efficient Data Retrieval
The system shall retrieve only the data required for the requested operation.

### NFR-PER-04 – Query Optimization
Database queries shall be designed to avoid unnecessary data loading and excessive database requests.

### NFR-PER-05 – Response Time
Under the agreed target environment and test load, normal read operations should complete within 2 seconds in typical cases.

## 4. Data Integrity and Reliability

### NFR-DATA-01 – Database Constraints
The system shall use database constraints and indexes to enforce data integrity where applicable.

### NFR-DATA-02 – Transactional Operations
Critical business operations, including invoice posting and inventory updates, shall execute within database transactions.

### NFR-DATA-03 – Concurrency Protection
The system shall protect critical inventory updates against concurrent modifications.

### NFR-DATA-04 – Historical Data Preservation
Historical business records shall be preserved when related master records are updated or deactivated.

### NFR-DATA-05 – Financial Precision
Financial calculations shall use decimal data types rather than floating-point types.

### NFR-DATA-06 – Audit Integrity
Audit records shall be protected against unauthorized modification or deletion.

### NFR-DATA-07 – Consistent Business State
The system shall prevent partially completed critical business operations from leaving inconsistent business data.

## 5. Usability Requirements

### NFR-USE-01 – Consistent Interface
The system shall provide a consistent user interface across its modules.

### NFR-USE-02 – Clear Feedback
The system shall provide clear success, validation, and error messages.

### NFR-USE-03 – Confirmation
The system shall request confirmation before sensitive operations.

### NFR-USE-04 – Navigation
The system shall provide organized navigation based on business modules and user roles.

### NFR-USE-05 – Form Validation
Forms shall clearly identify invalid fields and explain the required corrections.

## 6. Maintainability Requirements

### NFR-MNT-01 – Layered Architecture
The system shall follow the approved layered architecture and maintain separation of responsibilities.

### NFR-MNT-02 – Code Organization
The source code shall be organized into meaningful projects, folders, classes, and modules.

### NFR-MNT-03 – Reusable Components
Common business and application functionality shall be implemented in reusable components where appropriate.

### NFR-MNT-04 – Dependency Injection
The system shall use dependency injection to manage application services and infrastructure dependencies.

### NFR-MNT-05 – Documentation
The project shall maintain technical and business documentation that reflects the implemented system.

## 7. Compatibility and Responsiveness

### NFR-COMP-01 – Browser Compatibility
The system shall support modern versions of commonly used web browsers.

### NFR-COMP-02 – Responsive Interface
The user interface shall adapt to desktop, tablet, and mobile screen sizes.

### NFR-COMP-03 – Accessibility
The interface shall use readable typography, clear labels, keyboard-accessible controls, and appropriate visual contrast.

## 8. Backup and Recovery

### NFR-REC-01 – Database Backup
A documented SQL Server database backup procedure shall be provided.

### NFR-REC-02 – Database Recovery
A documented procedure for restoring the database from a valid backup shall be provided.

### NFR-REC-03 – Backup Verification
Database restoration procedures shall be verified before considering the backup process reliable.

## 9. Logging and Error Handling

### NFR-LOG-01 – Centralized Error Handling
The system shall handle application errors consistently.

### NFR-LOG-02 – Application Logging
The system shall record relevant application errors and operational events.

### NFR-LOG-03 – Safe Error Messages
The system shall avoid exposing internal implementation details or sensitive information in user-facing error messages.

## 10. Deployment and Configuration

### NFR-DEP-01 – Environment Configuration
Environment-specific settings shall be separated from application source code.

### NFR-DEP-02 – Secret Management
Connection strings and sensitive credentials shall not be committed to the public source repository.

### NFR-DEP-03 – Database Migrations
Database schema changes shall be managed through Entity Framework Core migrations.

## 11. Project Constraints

- The application will use ASP.NET Core MVC.
- The database will use SQL Server.
- The initial release will support a single company.
- The default currency will be EGP.
- The application will use a single deployment architecture.
- The initial release will not require online payment services.