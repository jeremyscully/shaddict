# Transitus API

A RESTful API for the Transitus application, providing access to vendor, purchase order, and invoice data.

## Features

- RESTful API endpoints for managing vendors, purchase orders, and invoices
- Swagger documentation for easy API exploration
- Secure database access using stored procedures
- Clean architecture with separation of concerns

## Technologies Used

- ASP.NET Core 7.0
- Swagger/OpenAPI for API documentation
- Dapper for efficient data access
- SQL Server for data storage

## Getting Started

### Prerequisites

- .NET 7.0 SDK
- SQL Server (LocalDB or full instance)
- The Transitus database with stored procedures

### Setup

1. Clone the repository
2. Update the connection string in `appsettings.json` if needed
3. Run the application:
   ```
   dotnet run
   ```
4. Access the Swagger UI at `http://localhost:5000`

## API Endpoints

The API provides the following endpoints:

### Vendors

- `GET /api/vendors` - Get all vendors
- `GET /api/vendors/{id}` - Get a specific vendor
- `POST /api/vendors` - Create a new vendor
- `PUT /api/vendors/{id}` - Update a vendor
- `DELETE /api/vendors/{id}` - Delete a vendor

### Future Endpoints

- Purchase Orders
- Invoices

## License

This project is licensed under the MIT License.