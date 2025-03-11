-- =============================================
-- Test Data Insertion Script for Transitus (Direct Inserts)
-- =============================================

SET NOCOUNT OFF;
PRINT 'Starting test data insertion...';

-- Check if domain data exists, if not insert it
PRINT 'Checking domain data...';
IF NOT EXISTS (SELECT 1 FROM VendorType)
BEGIN
    PRINT 'Inserting VendorType data...';
    -- Insert data into VendorType table
    INSERT INTO VendorType (TypeName, Description)
    VALUES 
    ('Supplier', 'Companies that supply goods or raw materials'),
    ('Service Provider', 'Companies that provide services'),
    ('Contractor', 'Independent contractors or freelancers'),
    ('Consultant', 'Professional consultants'),
    ('Distributor', 'Companies that distribute products');
    PRINT 'VendorType data inserted.';
END
ELSE
BEGIN
    PRINT 'VendorType data already exists.';
END

IF NOT EXISTS (SELECT 1 FROM PurchaseOrderType)
BEGIN
    PRINT 'Inserting PurchaseOrderType data...';
    -- Insert data into PurchaseOrderType table
    INSERT INTO PurchaseOrderType (TypeName, Description)
    VALUES 
    ('Standard', 'Regular purchase order for goods or services'),
    ('Blanket', 'Long-term agreement with predetermined terms'),
    ('Contract', 'Purchase order tied to a specific contract'),
    ('Emergency', 'Urgent purchase orders with expedited processing'),
    ('Direct', 'Direct purchase without bidding process');
    PRINT 'PurchaseOrderType data inserted.';
END
ELSE
BEGIN
    PRINT 'PurchaseOrderType data already exists.';
END

IF NOT EXISTS (SELECT 1 FROM InvoiceStatus)
BEGIN
    PRINT 'Inserting InvoiceStatus data...';
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
    PRINT 'InvoiceStatus data inserted.';
END
ELSE
BEGIN
    PRINT 'InvoiceStatus data already exists.';
END

-- =============================================
-- Insert Test Vendors
-- =============================================

PRINT 'Inserting test vendors...';

-- Declare variables for vendor IDs
DECLARE @VendorId1 INT;
DECLARE @VendorId2 INT;
DECLARE @VendorId3 INT;

-- Insert Vendor 1 - Office Supplies
PRINT 'Inserting Vendor 1...';
INSERT INTO Vendor (
    VendorTypeId, VendorName, VendorCode, TaxId, Website, Email, Phone,
    Address1, Address2, City, State, PostalCode, Country,
    PrimaryContactName, PrimaryContactEmail, PrimaryContactPhone,
    PaymentTerms, Notes, IsActive, CreatedBy, CreatedDate
)
VALUES (
    1, -- Supplier
    'Test Office Supplies Inc.',
    'TEST-OS-001',
    '12-3456789',
    'www.testofficesupplies.com',
    'info@testofficesupplies.com',
    '555-123-4567',
    '123 Main Street',
    'Suite 100',
    'Anytown',
    'CA',
    '90210',
    'USA',
    'John Smith',
    'john@testofficesupplies.com',
    '555-123-4568',
    'Net 30',
    'Reliable office supplies vendor',
    1,
    'System',
    GETDATE()
);
SET @VendorId1 = SCOPE_IDENTITY();
PRINT 'Vendor 1 inserted with ID: ' + CAST(@VendorId1 AS NVARCHAR(10));

-- Insert Vendor 2 - IT Services
PRINT 'Inserting Vendor 2...';
INSERT INTO Vendor (
    VendorTypeId, VendorName, VendorCode, TaxId, Website, Email, Phone,
    Address1, Address2, City, State, PostalCode, Country,
    PrimaryContactName, PrimaryContactEmail, PrimaryContactPhone,
    PaymentTerms, Notes, IsActive, CreatedBy, CreatedDate
)
VALUES (
    2, -- Service Provider
    'Test IT Solutions LLC',
    'TEST-IT-001',
    '98-7654321',
    'www.testitsolutions.com',
    'support@testitsolutions.com',
    '555-987-6543',
    '456 Tech Blvd',
    NULL,
    'Silicon Valley',
    'CA',
    '94025',
    'USA',
    'Jane Doe',
    'jane@testitsolutions.com',
    '555-987-6544',
    'Net 15',
    'IT services and hardware provider',
    1,
    'System',
    GETDATE()
);
SET @VendorId2 = SCOPE_IDENTITY();
PRINT 'Vendor 2 inserted with ID: ' + CAST(@VendorId2 AS NVARCHAR(10));

-- Insert Vendor 3 - Marketing Agency
PRINT 'Inserting Vendor 3...';
INSERT INTO Vendor (
    VendorTypeId, VendorName, VendorCode, TaxId, Website, Email, Phone,
    Address1, Address2, City, State, PostalCode, Country,
    PrimaryContactName, PrimaryContactEmail, PrimaryContactPhone,
    PaymentTerms, Notes, IsActive, CreatedBy, CreatedDate
)
VALUES (
    4, -- Consultant
    'Test Marketing Experts',
    'TEST-MK-001',
    '45-6789123',
    'www.testmarketingexperts.com',
    'hello@testmarketingexperts.com',
    '555-456-7890',
    '789 Creative Ave',
    'Floor 12',
    'Metropolis',
    'NY',
    '10001',
    'USA',
    'Robert Johnson',
    'robert@testmarketingexperts.com',
    '555-456-7891',
    'Net 45',
    'Marketing and advertising services',
    1,
    'System',
    GETDATE()
);
SET @VendorId3 = SCOPE_IDENTITY();
PRINT 'Vendor 3 inserted with ID: ' + CAST(@VendorId3 AS NVARCHAR(10));

-- =============================================
-- Insert Test Purchase Orders
-- =============================================

PRINT 'Inserting test purchase orders...';

-- Declare variables for purchase order IDs
DECLARE @POId1 INT;
DECLARE @POId2 INT;
DECLARE @POId3 INT;

-- Insert Purchase Order 1 - Office Supplies
PRINT 'Inserting Purchase Order 1...';
INSERT INTO PurchaseOrder (
    PurchaseOrderNumber, PurchaseOrderTypeId, VendorId, OrderDate, ExpectedDeliveryDate,
    ShippingAddress, BillingAddress, SubTotal, TaxAmount, ShippingAmount, TotalAmount,
    Currency, Status, ApprovalDate, ApprovedBy, Notes, IsActive, CreatedBy, CreatedDate
)
VALUES (
    'PO-TEST-001',
    1, -- Standard
    @VendorId1,
    DATEADD(day, -30, GETDATE()),
    DATEADD(day, -15, GETDATE()),
    '123 Corporate HQ, Anytown, CA 90210',
    '123 Corporate HQ, Anytown, CA 90210',
    1000.00,
    80.00,
    20.00,
    1100.00,
    'USD',
    'Completed',
    DATEADD(day, -29, GETDATE()),
    'Jane Manager',
    'Office supplies for Q1',
    1,
    'System',
    GETDATE()
);
SET @POId1 = SCOPE_IDENTITY();
PRINT 'Purchase Order 1 inserted with ID: ' + CAST(@POId1 AS NVARCHAR(10));

-- Insert Purchase Order 2 - IT Equipment
PRINT 'Inserting Purchase Order 2...';
INSERT INTO PurchaseOrder (
    PurchaseOrderNumber, PurchaseOrderTypeId, VendorId, OrderDate, ExpectedDeliveryDate,
    ShippingAddress, BillingAddress, SubTotal, TaxAmount, ShippingAmount, TotalAmount,
    Currency, Status, ApprovalDate, ApprovedBy, Notes, IsActive, CreatedBy, CreatedDate
)
VALUES (
    'PO-TEST-002',
    1, -- Standard
    @VendorId2,
    DATEADD(day, -15, GETDATE()),
    DATEADD(day, -5, GETDATE()),
    '123 Corporate HQ, Anytown, CA 90210',
    '123 Corporate HQ, Anytown, CA 90210',
    3500.00,
    280.00,
    0.00,
    3780.00,
    'USD',
    'Received',
    DATEADD(day, -14, GETDATE()),
    'John Director',
    'New laptops for IT department',
    1,
    'System',
    GETDATE()
);
SET @POId2 = SCOPE_IDENTITY();
PRINT 'Purchase Order 2 inserted with ID: ' + CAST(@POId2 AS NVARCHAR(10));

-- Insert Purchase Order 3 - Marketing Materials
PRINT 'Inserting Purchase Order 3...';
INSERT INTO PurchaseOrder (
    PurchaseOrderNumber, PurchaseOrderTypeId, VendorId, OrderDate, ExpectedDeliveryDate,
    ShippingAddress, BillingAddress, SubTotal, TaxAmount, ShippingAmount, TotalAmount,
    Currency, Status, ApprovalDate, ApprovedBy, Notes, IsActive, CreatedBy, CreatedDate
)
VALUES (
    'PO-TEST-003',
    3, -- Contract
    @VendorId3,
    DATEADD(day, -5, GETDATE()),
    DATEADD(day, 10, GETDATE()),
    '123 Corporate HQ, Anytown, CA 90210',
    '123 Corporate HQ, Anytown, CA 90210',
    2300.00,
    184.00,
    50.00,
    2534.00,
    'USD',
    'In Progress',
    DATEADD(day, -4, GETDATE()),
    'Sarah VP',
    'Marketing materials for trade show',
    1,
    'System',
    GETDATE()
);
SET @POId3 = SCOPE_IDENTITY();
PRINT 'Purchase Order 3 inserted with ID: ' + CAST(@POId3 AS NVARCHAR(10));

-- =============================================
-- Insert Purchase Order Items
-- =============================================

PRINT 'Inserting purchase order items...';

-- Declare variables for PO item IDs
DECLARE @POItem1 INT;
DECLARE @POItem2 INT;
DECLARE @POItem3 INT;
DECLARE @POItem4 INT;
DECLARE @POItem5 INT;

-- Insert PO Items for PO 1 (Office Supplies)
PRINT 'Inserting PO Item 1...';
INSERT INTO PurchaseOrderItem (
    PurchaseOrderId, ItemNumber, Description, Quantity, UnitPrice,
    UnitOfMeasure, TaxRate, TaxAmount, TotalAmount, Notes, CreatedDate
)
VALUES (
    @POId1,
    'PAPER-A4',
    'Premium A4 Paper, 500 sheets',
    10,
    5.00,
    'Box',
    8.00,
    4.00,
    54.00,
    'For office printers',
    GETDATE()
);
SET @POItem1 = SCOPE_IDENTITY();
PRINT 'PO Item 1 inserted with ID: ' + CAST(@POItem1 AS NVARCHAR(10));

PRINT 'Inserting PO Item 2...';
INSERT INTO PurchaseOrderItem (
    PurchaseOrderId, ItemNumber, Description, Quantity, UnitPrice,
    UnitOfMeasure, TaxRate, TaxAmount, TotalAmount, Notes, CreatedDate
)
VALUES (
    @POId1,
    'PEN-BLU',
    'Blue Ballpoint Pens',
    50,
    1.50,
    'Each',
    8.00,
    6.00,
    81.00,
    'Medium point',
    GETDATE()
);
SET @POItem2 = SCOPE_IDENTITY();
PRINT 'PO Item 2 inserted with ID: ' + CAST(@POItem2 AS NVARCHAR(10));

-- Insert PO Items for PO 2 (IT Equipment)
PRINT 'Inserting PO Item 3...';
INSERT INTO PurchaseOrderItem (
    PurchaseOrderId, ItemNumber, Description, Quantity, UnitPrice,
    UnitOfMeasure, TaxRate, TaxAmount, TotalAmount, Notes, CreatedDate
)
VALUES (
    @POId2,
    'LAPTOP-PRO',
    'Professional Laptop, 16GB RAM, 512GB SSD',
    3,
    1200.00,
    'Each',
    8.00,
    288.00,
    3888.00,
    'For development team',
    GETDATE()
);
SET @POItem3 = SCOPE_IDENTITY();
PRINT 'PO Item 3 inserted with ID: ' + CAST(@POItem3 AS NVARCHAR(10));

-- Insert PO Items for PO 3 (Marketing Materials)
PRINT 'Inserting PO Item 4...';
INSERT INTO PurchaseOrderItem (
    PurchaseOrderId, ItemNumber, Description, Quantity, UnitPrice,
    UnitOfMeasure, TaxRate, TaxAmount, TotalAmount, Notes, CreatedDate
)
VALUES (
    @POId3,
    'BROCHURE-A',
    'Full Color Brochures, Tri-fold',
    1000,
    1.20,
    'Each',
    8.00,
    96.00,
    1296.00,
    'For trade show',
    GETDATE()
);
SET @POItem4 = SCOPE_IDENTITY();
PRINT 'PO Item 4 inserted with ID: ' + CAST(@POItem4 AS NVARCHAR(10));

PRINT 'Inserting PO Item 5...';
INSERT INTO PurchaseOrderItem (
    PurchaseOrderId, ItemNumber, Description, Quantity, UnitPrice,
    UnitOfMeasure, TaxRate, TaxAmount, TotalAmount, Notes, CreatedDate
)
VALUES (
    @POId3,
    'BANNER-L',
    'Large Format Banner, 3x6 feet',
    2,
    150.00,
    'Each',
    8.00,
    24.00,
    324.00,
    'For booth display',
    GETDATE()
);
SET @POItem5 = SCOPE_IDENTITY();
PRINT 'PO Item 5 inserted with ID: ' + CAST(@POItem5 AS NVARCHAR(10));

-- =============================================
-- Insert Test Invoices
-- =============================================

PRINT 'Inserting test invoices...';

-- Declare variables for invoice IDs
DECLARE @InvoiceId1 INT;
DECLARE @InvoiceId2 INT;
DECLARE @InvoiceId3 INT;

-- Insert Invoice 1 - Office Supplies (Paid)
PRINT 'Inserting Invoice 1...';
INSERT INTO Invoice (
    InvoiceNumber, VendorId, PurchaseOrderId, InvoiceStatusId, InvoiceDate, DueDate,
    SubTotal, TaxAmount, ShippingAmount, TotalAmount, AmountPaid,
    Currency, PaymentDate, PaymentReference, Notes, IsActive, CreatedBy, CreatedDate
)
VALUES (
    'INV-TEST-001',
    @VendorId1,
    @POId1,
    6, -- Paid
    DATEADD(day, -25, GETDATE()),
    DATEADD(day, -10, GETDATE()),
    1000.00,
    80.00,
    20.00,
    1100.00,
    1100.00,
    'USD',
    DATEADD(day, -15, GETDATE()),
    'ACH-12345',
    'Office supplies - full payment',
    1,
    'System',
    GETDATE()
);
SET @InvoiceId1 = SCOPE_IDENTITY();
PRINT 'Invoice 1 inserted with ID: ' + CAST(@InvoiceId1 AS NVARCHAR(10));

-- Insert Invoice 2 - IT Equipment (Partially Paid)
PRINT 'Inserting Invoice 2...';
INSERT INTO Invoice (
    InvoiceNumber, VendorId, PurchaseOrderId, InvoiceStatusId, InvoiceDate, DueDate,
    SubTotal, TaxAmount, ShippingAmount, TotalAmount, AmountPaid,
    Currency, PaymentDate, PaymentReference, Notes, IsActive, CreatedBy, CreatedDate
)
VALUES (
    'INV-TEST-002',
    @VendorId2,
    @POId2,
    5, -- Partially Paid
    DATEADD(day, -10, GETDATE()),
    DATEADD(day, 5, GETDATE()),
    2000.00,
    160.00,
    0.00,
    2160.00,
    1000.00,
    'USD',
    DATEADD(day, -5, GETDATE()),
    'ACH-12346',
    'IT equipment - partial payment',
    1,
    'System',
    GETDATE()
);
SET @InvoiceId2 = SCOPE_IDENTITY();
PRINT 'Invoice 2 inserted with ID: ' + CAST(@InvoiceId2 AS NVARCHAR(10));

-- Insert Invoice 3 - IT Equipment (Remaining Balance)
PRINT 'Inserting Invoice 3...';
INSERT INTO Invoice (
    InvoiceNumber, VendorId, PurchaseOrderId, InvoiceStatusId, InvoiceDate, DueDate,
    SubTotal, TaxAmount, ShippingAmount, TotalAmount, AmountPaid,
    Currency, PaymentDate, PaymentReference, Notes, IsActive, CreatedBy, CreatedDate
)
VALUES (
    'INV-TEST-003',
    @VendorId2,
    @POId2,
    2, -- Submitted
    DATEADD(day, -10, GETDATE()),
    DATEADD(day, 5, GETDATE()),
    1620.00,
    129.60,
    0.00,
    1749.60,
    0.00,
    'USD',
    NULL,
    NULL,
    'IT equipment - remaining balance',
    1,
    'System',
    GETDATE()
);
SET @InvoiceId3 = SCOPE_IDENTITY();
PRINT 'Invoice 3 inserted with ID: ' + CAST(@InvoiceId3 AS NVARCHAR(10));

-- =============================================
-- Insert Invoice Items
-- =============================================

PRINT 'Inserting invoice items...';

-- Insert Invoice Items for Invoice 1 (Office Supplies)
PRINT 'Inserting Invoice Item 1...';
INSERT INTO InvoiceItem (
    InvoiceId, PurchaseOrderItemId, ItemNumber, Description, Quantity, UnitPrice,
    UnitOfMeasure, TaxRate, TaxAmount, TotalAmount, Notes, CreatedDate
)
VALUES (
    @InvoiceId1,
    @POItem1,
    'PAPER-A4',
    'Premium A4 Paper, 500 sheets',
    10,
    5.00,
    'Box',
    8.00,
    4.00,
    54.00,
    'For office printers',
    GETDATE()
);
DECLARE @InvoiceItem1 INT = SCOPE_IDENTITY();
PRINT 'Invoice Item 1 inserted with ID: ' + CAST(@InvoiceItem1 AS NVARCHAR(10));

PRINT 'Inserting Invoice Item 2...';
INSERT INTO InvoiceItem (
    InvoiceId, PurchaseOrderItemId, ItemNumber, Description, Quantity, UnitPrice,
    UnitOfMeasure, TaxRate, TaxAmount, TotalAmount, Notes, CreatedDate
)
VALUES (
    @InvoiceId1,
    @POItem2,
    'PEN-BLU',
    'Blue Ballpoint Pens',
    50,
    1.50,
    'Each',
    8.00,
    6.00,
    81.00,
    'Medium point',
    GETDATE()
);
DECLARE @InvoiceItem2 INT = SCOPE_IDENTITY();
PRINT 'Invoice Item 2 inserted with ID: ' + CAST(@InvoiceItem2 AS NVARCHAR(10));

-- Insert Invoice Items for Invoice 2 (IT Equipment - Partial)
PRINT 'Inserting Invoice Item 3...';
INSERT INTO InvoiceItem (
    InvoiceId, PurchaseOrderItemId, ItemNumber, Description, Quantity, UnitPrice,
    UnitOfMeasure, TaxRate, TaxAmount, TotalAmount, Notes, CreatedDate
)
VALUES (
    @InvoiceId2,
    @POItem3,
    'LAPTOP-PRO',
    'Professional Laptop, 16GB RAM, 512GB SSD',
    1.5,
    1200.00,
    'Each',
    8.00,
    144.00,
    1944.00,
    'Partial delivery',
    GETDATE()
);
DECLARE @InvoiceItem3 INT = SCOPE_IDENTITY();
PRINT 'Invoice Item 3 inserted with ID: ' + CAST(@InvoiceItem3 AS NVARCHAR(10));

-- Insert Invoice Items for Invoice 3 (IT Equipment - Remaining)
PRINT 'Inserting Invoice Item 4...';
INSERT INTO InvoiceItem (
    InvoiceId, PurchaseOrderItemId, ItemNumber, Description, Quantity, UnitPrice,
    UnitOfMeasure, TaxRate, TaxAmount, TotalAmount, Notes, CreatedDate
)
VALUES (
    @InvoiceId3,
    @POItem3,
    'LAPTOP-PRO',
    'Professional Laptop, 16GB RAM, 512GB SSD',
    1.5,
    1200.00,
    'Each',
    8.00,
    144.00,
    1944.00,
    'Remaining delivery',
    GETDATE()
);
DECLARE @InvoiceItem4 INT = SCOPE_IDENTITY();
PRINT 'Invoice Item 4 inserted with ID: ' + CAST(@InvoiceItem4 AS NVARCHAR(10));

-- Print success message
PRINT 'Test data has been successfully inserted.';
PRINT 'Inserted 3 vendors, 3 purchase orders, 5 purchase order items, 3 invoices, and 4 invoice items.'; 