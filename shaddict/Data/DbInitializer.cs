using Microsoft.EntityFrameworkCore;
using Shaddict.Models;

namespace Shaddict.Data
{
    /// <summary>
    /// Initializes the database with sample data
    /// </summary>
    public static class DbInitializer
    {
        /// <summary>
        /// Initialize the database
        /// </summary>
        /// <param name="context">The database context</param>
        public static void Initialize(ShadDictContext context)
        {
            // Create the database if it doesn't exist
            // context.Database.EnsureDeleted(); // Removing this line to prevent database deletion on every startup
            context.Database.EnsureCreated();

            // Check if there are any entities
            if (context.Entities.Any())
            {
                return; // Database has been seeded
            }

            // Add sample entities
            var entities = new Entity[]
            {
                new Entity { Name = "Finance", Description = "Financial data and metrics" },
                new Entity { Name = "HR", Description = "Human Resources data" },
                new Entity { Name = "Sales", Description = "Sales and customer data" },
                new Entity { Name = "Operations", Description = "Operational data" }
            };

            context.Entities.AddRange(entities);
            context.SaveChanges();

            // Add sample tables
            var tables = new DatabaseTable[]
            {
                new DatabaseTable { 
                    Schema = "fin", 
                    Name = "Transactions", 
                    Description = "Financial transactions", 
                    EntityId = entities[0].Id 
                },
                new DatabaseTable { 
                    Schema = "fin", 
                    Name = "Accounts", 
                    Description = "Financial accounts", 
                    EntityId = entities[0].Id 
                },
                new DatabaseTable { 
                    Schema = "hr", 
                    Name = "Employees", 
                    Description = "Employee information", 
                    EntityId = entities[1].Id 
                },
                new DatabaseTable { 
                    Schema = "sales", 
                    Name = "Orders", 
                    Description = "Customer orders", 
                    EntityId = entities[2].Id 
                },
                new DatabaseTable { 
                    Schema = "sales", 
                    Name = "Customers", 
                    Description = "Customer information", 
                    EntityId = entities[2].Id 
                }
            };

            context.DatabaseTables.AddRange(tables);
            context.SaveChanges();

            // Add sample columns
            var columns = new TableColumn[]
            {
                // Transactions table columns
                new TableColumn { 
                    Name = "TransactionId", 
                    DataType = "int", 
                    IsNullable = false, 
                    IsPrimaryKey = true, 
                    Description = "Unique identifier for the transaction", 
                    TableId = tables[0].Id 
                },
                new TableColumn { 
                    Name = "TransactionDate", 
                    DataType = "datetime", 
                    IsNullable = false, 
                    Description = "Date and time of the transaction", 
                    TableId = tables[0].Id 
                },
                new TableColumn { 
                    Name = "Amount", 
                    DataType = "decimal", 
                    Precision = 18, 
                    Scale = 2, 
                    IsNullable = false, 
                    Description = "Transaction amount", 
                    ExampleValues = "100.00, 250.50, 1000.00", 
                    TableId = tables[0].Id 
                },
                
                // Accounts table columns
                new TableColumn { 
                    Name = "AccountId", 
                    DataType = "int", 
                    IsNullable = false, 
                    IsPrimaryKey = true, 
                    Description = "Unique identifier for the account", 
                    TableId = tables[1].Id 
                },
                new TableColumn { 
                    Name = "AccountName", 
                    DataType = "varchar", 
                    MaxLength = 100, 
                    IsNullable = false, 
                    Description = "Name of the account", 
                    TableId = tables[1].Id 
                },
                
                // Employees table columns
                new TableColumn { 
                    Name = "EmployeeId", 
                    DataType = "int", 
                    IsNullable = false, 
                    IsPrimaryKey = true, 
                    Description = "Unique identifier for the employee", 
                    TableId = tables[2].Id 
                },
                new TableColumn { 
                    Name = "FirstName", 
                    DataType = "varchar", 
                    MaxLength = 50, 
                    IsNullable = false, 
                    Description = "Employee's first name", 
                    TableId = tables[2].Id 
                },
                new TableColumn { 
                    Name = "LastName", 
                    DataType = "varchar", 
                    MaxLength = 50, 
                    IsNullable = false, 
                    Description = "Employee's last name", 
                    TableId = tables[2].Id 
                },
                
                // Orders table columns
                new TableColumn { 
                    Name = "OrderId", 
                    DataType = "int", 
                    IsNullable = false, 
                    IsPrimaryKey = true, 
                    Description = "Unique identifier for the order", 
                    TableId = tables[3].Id 
                },
                new TableColumn { 
                    Name = "OrderDate", 
                    DataType = "datetime", 
                    IsNullable = false, 
                    Description = "Date and time of the order", 
                    TableId = tables[3].Id 
                },
                new TableColumn { 
                    Name = "CustomerId", 
                    DataType = "int", 
                    IsNullable = false, 
                    IsForeignKey = true, 
                    Description = "Foreign key to the customer", 
                    TableId = tables[3].Id 
                },
                new TableColumn { 
                    Name = "TotalAmount", 
                    DataType = "decimal", 
                    Precision = 18, 
                    Scale = 2, 
                    IsNullable = false, 
                    Description = "Total amount of the order", 
                    ComputationFormula = "SUM(OrderItems.Price * OrderItems.Quantity)", 
                    TableId = tables[3].Id 
                },
                
                // Customers table columns
                new TableColumn { 
                    Name = "CustomerId", 
                    DataType = "int", 
                    IsNullable = false, 
                    IsPrimaryKey = true, 
                    Description = "Unique identifier for the customer", 
                    TableId = tables[4].Id 
                },
                new TableColumn { 
                    Name = "CustomerName", 
                    DataType = "varchar", 
                    MaxLength = 100, 
                    IsNullable = false, 
                    Description = "Name of the customer", 
                    TableId = tables[4].Id 
                },
                new TableColumn { 
                    Name = "Email", 
                    DataType = "varchar", 
                    MaxLength = 100, 
                    IsNullable = true, 
                    Description = "Customer's email address", 
                    TableId = tables[4].Id 
                }
            };

            context.TableColumns.AddRange(columns);
            context.SaveChanges();
        }
    }
} 