# Transitus Test Data

This folder contains scripts for creating and loading test data into the Transitus database.

## Files

- `insert_test_data_direct.sql` - SQL script that directly inserts test data into the database tables
- `load_test_data_direct.ps1` - PowerShell script to execute the SQL script against the database
- `verify_test_data.sql` - SQL script to verify the test data was inserted correctly

## Test Data Overview

The test data includes:

- **Domain Data**:
  - 5 Vendor Types (Supplier, Service Provider, Contractor, Consultant, Distributor)
  - 5 Purchase Order Types (Standard, Blanket, Contract, Emergency, Direct)
  - 9 Invoice Statuses (Draft, Submitted, Under Review, Approved, Partially Paid, Paid, Rejected, Cancelled, Overdue)

- **Vendors**: 3 test vendors
  - Test Office Supplies Inc. (Supplier)
  - Test IT Solutions LLC (Service Provider)
  - Test Marketing Experts (Consultant)

- **Purchase Orders**: 3 test purchase orders
  - PO-TEST-001: Office supplies from Test Office Supplies Inc.
  - PO-TEST-002: IT equipment from Test IT Solutions LLC
  - PO-TEST-003: Marketing materials from Test Marketing Experts

- **Purchase Order Items**: 5 items across the purchase orders
  - Paper and pens for PO-TEST-001
  - Laptops for PO-TEST-002
  - Brochures and banners for PO-TEST-003

- **Invoices**: 3 test invoices
  - INV-TEST-001: Fully paid invoice for office supplies
  - INV-TEST-002: Partially paid invoice for IT equipment
  - INV-TEST-003: Submitted invoice for remaining IT equipment

- **Invoice Items**: 4 items across the invoices

## Loading Test Data

### Prerequisites

- SQL Server LocalDB installed
- Transitus database created with the correct schema

### Using PowerShell

1. Open PowerShell in the directory containing the scripts
2. Run the PowerShell script:
   ```
   .\load_test_data_direct.ps1
   ```
3. The script will execute the SQL script and display the results
4. Output will be saved to `test_data_output.txt`

### Using SQL Server Management Studio (SSMS)

1. Open SSMS and connect to your LocalDB instance
2. Open the `insert_test_data_direct.sql` file
3. Select the Transitus database from the dropdown
4. Execute the script

## Verifying Test Data

To verify the test data was inserted correctly:

1. Run the verification script:
   ```
   sqlcmd -S "(localdb)\MSSQLLocalDB" -d "Transitus" -i ".\verify_test_data.sql" -o ".\verification_results.txt"
   ```
2. Check the `verification_results.txt` file for the results

## Notes

- The script checks for existing domain data before inserting, so it can be run multiple times without creating duplicates
- To reset test data, you can delete all records with names/numbers containing "Test" or "TEST" and run the script again
- The script uses direct INSERT statements rather than stored procedures for simplicity and reliability

## Troubleshooting

- If you encounter errors about missing tables, ensure the database schema has been created correctly
- If you see duplicate key errors, some test data may already exist - consider cleaning the database first
- For any other issues, check the output file for specific error messages 