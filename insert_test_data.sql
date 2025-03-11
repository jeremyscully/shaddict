-- =============================================
-- Test Data Insertion Script for Transitus
-- =============================================

-- Check if domain data exists, if not insert it
IF NOT EXISTS (SELECT 1 FROM VendorType)
BEGIN
    -- Insert data into VendorType table
    INSERT INTO VendorType (TypeName, Description)
    VALUES 
    ('Supplier', 'Companies that supply goods or raw materials'),
    ('Service Provider', 'Companies that provide services'),
    ('Contractor', 'Independent contractors or freelancers'),
    ('Consultant', 'Professional consultants'),
    ('Distributor', 'Companies that distribute products');
END

IF NOT EXISTS (SELECT 1 FROM PurchaseOrderType)
BEGIN
    -- Insert data into PurchaseOrderType table
    INSERT INTO PurchaseOrderType (TypeName, Description)
    VALUES 
    ('Standard', 'Regular purchase order for goods or services'),
    ('Blanket', 'Long-term agreement with predetermined terms'),
    ('Contract', 'Purchase order tied to a specific contract'),
    ('Emergency', 'Urgent purchase orders with expedited processing'),
    ('Direct', 'Direct purchase without bidding process');
END

IF NOT EXISTS (SELECT 1 FROM InvoiceStatus)
BEGIN
    -- Insert data into InvoiceStatus table
    INSERT INTO InvoiceStatus (StatusName, Description)
    VALUES 
    ('Draft', 'Invoice is in draft state and not yet submitted'),
    ('Submitted', 'Invoice has been submitted for approval'),
    ('Under Review', 'Invoice is being reviewed'),
    ('Approved', 'Invoice has been approved for payment'),
    ('Partially Paid', 'Invoice has been partially paid'),
    ('Paid', 'Invoice has been fully paid'),
    ('Rejected', 'Invoice has been rejected'),
    ('Cancelled', 'Invoice has been cancelled'),
    ('Overdue', 'Payment for the invoice is overdue');
END

-- =============================================
-- Insert Test Vendors
-- =============================================

-- Clear existing test data if needed (uncomment if you want to reset data)
-- DELETE FROM InvoiceItem;
-- DELETE FROM Invoice;
-- DELETE FROM PurchaseOrderItem;
-- DELETE FROM PurchaseOrder;
-- DELETE FROM Vendor WHERE VendorName LIKE 'Test%';

-- Declare variables for vendor IDs
DECLARE @VendorId1 INT;
DECLARE @VendorId2 INT;
DECLARE @VendorId3 INT;

-- Insert Vendor 1 - Office Supplies
DECLARE @VendorParams1 TABLE (VendorId INT);
INSERT INTO @VendorParams1 EXEC sp_UpsertVendor 
    @VendorId = 0,
    @VendorTypeId = 1, -- Supplier
    @VendorName = 'Test Office Supplies Inc.',
    @VendorCode = 'TEST-OS-001',
    @TaxId = '12-3456789',
    @Website = 'www.testofficesupplies.com',
    @Email = 'info@testofficesupplies.com',
    @Phone = '555-123-4567',
    @Address1 = '123 Main Street',
    @Address2 = 'Suite 100',
    @City = 'Anytown',
    @State = 'CA',
    @PostalCode = '90210',
    @Country = 'USA',
    @PrimaryContactName = 'John Smith',
    @PrimaryContactEmail = 'john@testofficesupplies.com',
    @PrimaryContactPhone = '555-123-4568',
    @PaymentTerms = 'Net 30',
    @Notes = 'Reliable office supplies vendor',
    @IsActive = 1,
    @ModifiedBy = 'System';
SELECT @VendorId1 = VendorId FROM @VendorParams1;

-- Insert Vendor 2 - IT Services
DECLARE @VendorParams2 TABLE (VendorId INT);
INSERT INTO @VendorParams2 EXEC sp_UpsertVendor 
    @VendorId = 0,
    @VendorTypeId = 2, -- Service Provider
    @VendorName = 'Test IT Solutions LLC',
    @VendorCode = 'TEST-IT-001',
    @TaxId = '98-7654321',
    @Website = 'www.testitsolutions.com',
    @Email = 'support@testitsolutions.com',
    @Phone = '555-987-6543',
    @Address1 = '456 Tech Blvd',
    @Address2 = NULL,
    @City = 'Silicon Valley',
    @State = 'CA',
    @PostalCode = '94025',
    @Country = 'USA',
    @PrimaryContactName = 'Jane Doe',
    @PrimaryContactEmail = 'jane@testitsolutions.com',
    @PrimaryContactPhone = '555-987-6544',
    @PaymentTerms = 'Net 15',
    @Notes = 'IT services and hardware provider',
    @IsActive = 1,
    @ModifiedBy = 'System';
SELECT @VendorId2 = VendorId FROM @VendorParams2;

-- Insert Vendor 3 - Marketing Agency
DECLARE @VendorParams3 TABLE (VendorId INT);
INSERT INTO @VendorParams3 EXEC sp_UpsertVendor 
    @VendorId = 0,
    @VendorTypeId = 4, -- Consultant
    @VendorName = 'Test Marketing Experts',
    @VendorCode = 'TEST-MK-001',
    @TaxId = '45-6789123',
    @Website = 'www.testmarketingexperts.com',
    @Email = 'hello@testmarketingexperts.com',
    @Phone = '555-456-7890',
    @Address1 = '789 Creative Ave',
    @Address2 = 'Floor 12',
    @City = 'Metropolis',
    @State = 'NY',
    @PostalCode = '10001',
    @Country = 'USA',
    @PrimaryContactName = 'Robert Johnson',
    @PrimaryContactEmail = 'robert@testmarketingexperts.com',
    @PrimaryContactPhone = '555-456-7891',
    @PaymentTerms = 'Net 45',
    @Notes = 'Marketing and advertising services',
    @IsActive = 1,
    @ModifiedBy = 'System';
SELECT @VendorId3 = VendorId FROM @VendorParams3;

-- =============================================
-- Insert Test Purchase Orders
-- =============================================

-- Declare variables for purchase order IDs
DECLARE @POId1 INT;
DECLARE @POId2 INT;
DECLARE @POId3 INT;

-- Insert Purchase Order 1 - Office Supplies
DECLARE @POParams1 TABLE (PurchaseOrderId INT);
INSERT INTO @POParams1 EXEC sp_UpsertPurchaseOrder
    @PurchaseOrderId = 0,
    @PurchaseOrderNumber = 'PO-TEST-001',
    @PurchaseOrderTypeId = 1, -- Standard
    @VendorId = @VendorId1,
    @OrderDate = DATEADD(dd, -30, GETDATE()),
    @ExpectedDeliveryDate = DATEADD(dd, -15, GETDATE()),
    @ShippingAddress = '123 Corporate HQ, Anytown, CA 90210',
    @BillingAddress = '123 Corporate HQ, Anytown, CA 90210',
    @SubTotal = 1000.00,
    @TaxAmount = 80.00,
    @ShippingAmount = 20.00,
    @TotalAmount = 1100.00,
    @Currency = 'USD',
    @Status = 'Completed',
    @ApprovalDate = DATEADD(dd, -29, GETDATE()),
    @ApprovedBy = 'Jane Manager',
    @Notes = 'Office supplies for Q1',
    @IsActive = 1,
    @ModifiedBy = 'System';
SELECT @POId1 = PurchaseOrderId FROM @POParams1;

-- Insert Purchase Order 2 - IT Equipment
DECLARE @POParams2 TABLE (PurchaseOrderId INT);
INSERT INTO @POParams2 EXEC sp_UpsertPurchaseOrder
    @PurchaseOrderId = 0,
    @PurchaseOrderNumber = 'PO-TEST-002',
    @PurchaseOrderTypeId = 1, -- Standard
    @VendorId = @VendorId2,
    @OrderDate = DATEADD(dd, -15, GETDATE()),
    @ExpectedDeliveryDate = DATEADD(dd, -5, GETDATE()),
    @ShippingAddress = '123 Corporate HQ, Anytown, CA 90210',
    @BillingAddress = '123 Corporate HQ, Anytown, CA 90210',
    @SubTotal = 3500.00,
    @TaxAmount = 280.00,
    @ShippingAmount = 0.00,
    @TotalAmount = 3780.00,
    @Currency = 'USD',
    @Status = 'Received',
    @ApprovalDate = DATEADD(dd, -14, GETDATE()),
    @ApprovedBy = 'John Director',
    @Notes = 'New laptops for IT department',
    @IsActive = 1,
    @ModifiedBy = 'System';
SELECT @POId2 = PurchaseOrderId FROM @POParams2;

-- Insert Purchase Order 3 - Marketing Materials
DECLARE @POParams3 TABLE (PurchaseOrderId INT);
INSERT INTO @POParams3 EXEC sp_UpsertPurchaseOrder
    @PurchaseOrderId = 0,
    @PurchaseOrderNumber = 'PO-TEST-003',
    @PurchaseOrderTypeId = 3, -- Contract
    @VendorId = @VendorId3,
    @OrderDate = DATEADD(dd, -5, GETDATE()),
    @ExpectedDeliveryDate = DATEADD(dd, 10, GETDATE()),
    @ShippingAddress = '123 Corporate HQ, Anytown, CA 90210',
    @BillingAddress = '123 Corporate HQ, Anytown, CA 90210',
    @SubTotal = 2300.00,
    @TaxAmount = 184.00,
    @ShippingAmount = 50.00,
    @TotalAmount = 2534.00,
    @Currency = 'USD',
    @Status = 'In Progress',
    @ApprovalDate = DATEADD(dd, -4, GETDATE()),
    @ApprovedBy = 'Sarah VP',
    @Notes = 'Marketing materials for trade show',
    @IsActive = 1,
    @ModifiedBy = 'System';
SELECT @POId3 = PurchaseOrderId FROM @POParams3;

-- =============================================
-- Insert Purchase Order Items
-- =============================================

-- Declare variables for PO item IDs
DECLARE @POItem1 INT;
DECLARE @POItem2 INT;
DECLARE @POItem3 INT;
DECLARE @POItem4 INT;
DECLARE @POItem5 INT;

-- Insert PO Items for PO 1 (Office Supplies)
DECLARE @POItemParams1 TABLE (PurchaseOrderItemId INT);
INSERT INTO @POItemParams1 EXEC sp_InsertPurchaseOrderItem
    @PurchaseOrderItemId = 0,
    @PurchaseOrderId = @POId1,
    @ItemNumber = 'PAPER-A4',
    @Description = 'Premium A4 Paper, 500 sheets',
    @Quantity = 10,
    @UnitPrice = 5.00,
    @UnitOfMeasure = 'Box',
    @TaxRate = 8.00,
    @TaxAmount = 4.00,
    @TotalAmount = 54.00,
    @Notes = 'For office printers';
SELECT @POItem1 = PurchaseOrderItemId FROM @POItemParams1;

DECLARE @POItemParams2 TABLE (PurchaseOrderItemId INT);
INSERT INTO @POItemParams2 EXEC sp_InsertPurchaseOrderItem
    @PurchaseOrderItemId = 0,
    @PurchaseOrderId = @POId1,
    @ItemNumber = 'PEN-BLU',
    @Description = 'Blue Ballpoint Pens',
    @Quantity = 50,
    @UnitPrice = 1.50,
    @UnitOfMeasure = 'Each',
    @TaxRate = 8.00,
    @TaxAmount = 6.00,
    @TotalAmount = 81.00,
    @Notes = 'Medium point';
SELECT @POItem2 = PurchaseOrderItemId FROM @POItemParams2;

-- Insert PO Items for PO 2 (IT Equipment)
DECLARE @POItemParams3 TABLE (PurchaseOrderItemId INT);
INSERT INTO @POItemParams3 EXEC sp_InsertPurchaseOrderItem
    @PurchaseOrderItemId = 0,
    @PurchaseOrderId = @POId2,
    @ItemNumber = 'LAPTOP-PRO',
    @Description = 'Professional Laptop, 16GB RAM, 512GB SSD',
    @Quantity = 3,
    @UnitPrice = 1200.00,
    @UnitOfMeasure = 'Each',
    @TaxRate = 8.00,
    @TaxAmount = 288.00,
    @TotalAmount = 3888.00,
    @Notes = 'For development team';
SELECT @POItem3 = PurchaseOrderItemId FROM @POItemParams3;

-- Insert PO Items for PO 3 (Marketing Materials)
DECLARE @POItemParams4 TABLE (PurchaseOrderItemId INT);
INSERT INTO @POItemParams4 EXEC sp_InsertPurchaseOrderItem
    @PurchaseOrderItemId = 0,
    @PurchaseOrderId = @POId3,
    @ItemNumber = 'BROCHURE-A',
    @Description = 'Full Color Brochures, Tri-fold',
    @Quantity = 1000,
    @UnitPrice = 1.20,
    @UnitOfMeasure = 'Each',
    @TaxRate = 8.00,
    @TaxAmount = 96.00,
    @TotalAmount = 1296.00,
    @Notes = 'For trade show';
SELECT @POItem4 = PurchaseOrderItemId FROM @POItemParams4;

DECLARE @POItemParams5 TABLE (PurchaseOrderItemId INT);
INSERT INTO @POItemParams5 EXEC sp_InsertPurchaseOrderItem
    @PurchaseOrderItemId = 0,
    @PurchaseOrderId = @POId3,
    @ItemNumber = 'BANNER-L',
    @Description = 'Large Format Banner, 3x6 feet',
    @Quantity = 2,
    @UnitPrice = 150.00,
    @UnitOfMeasure = 'Each',
    @TaxRate = 8.00,
    @TaxAmount = 24.00,
    @TotalAmount = 324.00,
    @Notes = 'For booth display';
SELECT @POItem5 = PurchaseOrderItemId FROM @POItemParams5;

-- =============================================
-- Insert Test Invoices
-- =============================================

-- Declare variables for invoice IDs
DECLARE @InvoiceId1 INT;
DECLARE @InvoiceId2 INT;
DECLARE @InvoiceId3 INT;

-- Insert Invoice 1 - Office Supplies (Paid)
DECLARE @InvoiceParams1 TABLE (InvoiceId INT);
INSERT INTO @InvoiceParams1 EXEC sp_UpsertInvoice
    @InvoiceId = 0,
    @InvoiceNumber = 'INV-TEST-001',
    @VendorId = @VendorId1,
    @PurchaseOrderId = @POId1,
    @InvoiceStatusId = 6, -- Paid
    @InvoiceDate = DATEADD(dd, -25, GETDATE()),
    @DueDate = DATEADD(dd, -10, GETDATE()),
    @SubTotal = 1000.00,
    @TaxAmount = 80.00,
    @ShippingAmount = 20.00,
    @TotalAmount = 1100.00,
    @AmountPaid = 1100.00,
    @Currency = 'USD',
    @PaymentDate = DATEADD(dd, -15, GETDATE()),
    @PaymentReference = 'ACH-12345',
    @Notes = 'Office supplies - full payment',
    @IsActive = 1,
    @ModifiedBy = 'System';
SELECT @InvoiceId1 = InvoiceId FROM @InvoiceParams1;

-- Insert Invoice 2 - IT Equipment (Partially Paid)
DECLARE @InvoiceParams2 TABLE (InvoiceId INT);
INSERT INTO @InvoiceParams2 EXEC sp_UpsertInvoice
    @InvoiceId = 0,
    @InvoiceNumber = 'INV-TEST-002',
    @VendorId = @VendorId2,
    @PurchaseOrderId = @POId2,
    @InvoiceStatusId = 5, -- Partially Paid
    @InvoiceDate = DATEADD(dd, -10, GETDATE()),
    @DueDate = DATEADD(dd, 5, GETDATE()),
    @SubTotal = 2000.00,
    @TaxAmount = 160.00,
    @ShippingAmount = 0.00,
    @TotalAmount = 2160.00,
    @AmountPaid = 1000.00,
    @Currency = 'USD',
    @PaymentDate = DATEADD(dd, -5, GETDATE()),
    @PaymentReference = 'ACH-12346',
    @Notes = 'IT equipment - partial payment',
    @IsActive = 1,
    @ModifiedBy = 'System';
SELECT @InvoiceId2 = InvoiceId FROM @InvoiceParams2;

-- Insert Invoice 3 - IT Equipment (Remaining Balance)
DECLARE @InvoiceParams3 TABLE (InvoiceId INT);
INSERT INTO @InvoiceParams3 EXEC sp_UpsertInvoice
    @InvoiceId = 0,
    @InvoiceNumber = 'INV-TEST-003',
    @VendorId = @VendorId2,
    @PurchaseOrderId = @POId2,
    @InvoiceStatusId = 2, -- Submitted
    @InvoiceDate = DATEADD(dd, -10, GETDATE()),
    @DueDate = DATEADD(dd, 5, GETDATE()),
    @SubTotal = 1620.00,
    @TaxAmount = 129.60,
    @ShippingAmount = 0.00,
    @TotalAmount = 1749.60,
    @AmountPaid = 0.00,
    @Currency = 'USD',
    @PaymentDate = NULL,
    @PaymentReference = NULL,
    @Notes = 'IT equipment - remaining balance',
    @IsActive = 1,
    @ModifiedBy = 'System';
SELECT @InvoiceId3 = InvoiceId FROM @InvoiceParams3;

-- =============================================
-- Insert Invoice Items
-- =============================================

-- Insert Invoice Items for Invoice 1 (Office Supplies)
DECLARE @InvoiceItem1 INT;
DECLARE @InvoiceItemParams1 TABLE (InvoiceItemId INT);
INSERT INTO @InvoiceItemParams1 EXEC sp_InsertInvoiceItem
    @InvoiceItemId = 0,
    @InvoiceId = @InvoiceId1,
    @PurchaseOrderItemId = @POItem1,
    @ItemNumber = 'PAPER-A4',
    @Description = 'Premium A4 Paper, 500 sheets',
    @Quantity = 10,
    @UnitPrice = 5.00,
    @UnitOfMeasure = 'Box',
    @TaxRate = 8.00,
    @TaxAmount = 4.00,
    @TotalAmount = 54.00,
    @Notes = 'For office printers';
SELECT @InvoiceItem1 = InvoiceItemId FROM @InvoiceItemParams1;

DECLARE @InvoiceItem2 INT;
DECLARE @InvoiceItemParams2 TABLE (InvoiceItemId INT);
INSERT INTO @InvoiceItemParams2 EXEC sp_InsertInvoiceItem
    @InvoiceItemId = 0,
    @InvoiceId = @InvoiceId1,
    @PurchaseOrderItemId = @POItem2,
    @ItemNumber = 'PEN-BLU',
    @Description = 'Blue Ballpoint Pens',
    @Quantity = 50,
    @UnitPrice = 1.50,
    @UnitOfMeasure = 'Each',
    @TaxRate = 8.00,
    @TaxAmount = 6.00,
    @TotalAmount = 81.00,
    @Notes = 'Medium point';
SELECT @InvoiceItem2 = InvoiceItemId FROM @InvoiceItemParams2;

-- Insert Invoice Items for Invoice 2 (IT Equipment - Partial)
DECLARE @InvoiceItem3 INT;
DECLARE @InvoiceItemParams3 TABLE (InvoiceItemId INT);
INSERT INTO @InvoiceItemParams3 EXEC sp_InsertInvoiceItem
    @InvoiceItemId = 0,
    @InvoiceId = @InvoiceId2,
    @PurchaseOrderItemId = @POItem3,
    @ItemNumber = 'LAPTOP-PRO',
    @Description = 'Professional Laptop, 16GB RAM, 512GB SSD',
    @Quantity = 1.5,
    @UnitPrice = 1200.00,
    @UnitOfMeasure = 'Each',
    @TaxRate = 8.00,
    @TaxAmount = 144.00,
    @TotalAmount = 1944.00,
    @Notes = 'Partial delivery';
SELECT @InvoiceItem3 = InvoiceItemId FROM @InvoiceItemParams3;

-- Insert Invoice Items for Invoice 3 (IT Equipment - Remaining)
DECLARE @InvoiceItem4 INT;
DECLARE @InvoiceItemParams4 TABLE (InvoiceItemId INT);
INSERT INTO @InvoiceItemParams4 EXEC sp_InsertInvoiceItem
    @InvoiceItemId = 0,
    @InvoiceId = @InvoiceId3,
    @PurchaseOrderItemId = @POItem3,
    @ItemNumber = 'LAPTOP-PRO',
    @Description = 'Professional Laptop, 16GB RAM, 512GB SSD',
    @Quantity = 1.5,
    @UnitPrice = 1200.00,
    @UnitOfMeasure = 'Each',
    @TaxRate = 8.00,
    @TaxAmount = 144.00,
    @TotalAmount = 1944.00,
    @Notes = 'Remaining delivery';
SELECT @InvoiceItem4 = InvoiceItemId FROM @InvoiceItemParams4;

-- Print success message
PRINT 'Test data has been successfully inserted.';
PRINT 'Inserted 3 vendors, 3 purchase orders, 5 purchase order items, 3 invoices, and 4 invoice items.'; 