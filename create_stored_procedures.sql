-- =============================================
-- Vendor Stored Procedures
-- =============================================

-- Get all vendors
CREATE PROCEDURE sp_GetVendors
AS
BEGIN
    SET NOCOUNT ON;
    SELECT v.*, vt.TypeName as VendorTypeName
    FROM Vendor v
    INNER JOIN VendorType vt ON v.VendorTypeId = vt.VendorTypeId
    WHERE v.IsActive = 1
    ORDER BY v.VendorName;
END
GO

-- Get vendor by ID
CREATE PROCEDURE sp_GetVendorById
    @VendorId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT v.*, vt.TypeName as VendorTypeName
    FROM Vendor v
    INNER JOIN VendorType vt ON v.VendorTypeId = vt.VendorTypeId
    WHERE v.VendorId = @VendorId;
END
GO

-- Insert or update vendor
CREATE PROCEDURE sp_UpsertVendor
    @VendorId INT OUTPUT,
    @VendorTypeId INT,
    @VendorName NVARCHAR(100),
    @VendorCode NVARCHAR(20),
    @TaxId NVARCHAR(20) = NULL,
    @Website NVARCHAR(255) = NULL,
    @Email NVARCHAR(100) = NULL,
    @Phone NVARCHAR(20) = NULL,
    @Address1 NVARCHAR(100) = NULL,
    @Address2 NVARCHAR(100) = NULL,
    @City NVARCHAR(50) = NULL,
    @State NVARCHAR(50) = NULL,
    @PostalCode NVARCHAR(20) = NULL,
    @Country NVARCHAR(50) = NULL,
    @PrimaryContactName NVARCHAR(100) = NULL,
    @PrimaryContactEmail NVARCHAR(100) = NULL,
    @PrimaryContactPhone NVARCHAR(20) = NULL,
    @PaymentTerms NVARCHAR(50) = NULL,
    @Notes NVARCHAR(MAX) = NULL,
    @IsActive BIT = 1,
    @ModifiedBy NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @VendorId = 0
    BEGIN
        -- Insert new vendor
        INSERT INTO Vendor (
            VendorTypeId, VendorName, VendorCode, TaxId, Website, Email, Phone,
            Address1, Address2, City, State, PostalCode, Country,
            PrimaryContactName, PrimaryContactEmail, PrimaryContactPhone,
            PaymentTerms, Notes, IsActive, CreatedBy, CreatedDate
        )
        VALUES (
            @VendorTypeId, @VendorName, @VendorCode, @TaxId, @Website, @Email, @Phone,
            @Address1, @Address2, @City, @State, @PostalCode, @Country,
            @PrimaryContactName, @PrimaryContactEmail, @PrimaryContactPhone,
            @PaymentTerms, @Notes, @IsActive, @ModifiedBy, GETDATE()
        );
        
        SET @VendorId = SCOPE_IDENTITY();
    END
    ELSE
    BEGIN
        -- Update existing vendor
        UPDATE Vendor
        SET VendorTypeId = @VendorTypeId,
            VendorName = @VendorName,
            VendorCode = @VendorCode,
            TaxId = @TaxId,
            Website = @Website,
            Email = @Email,
            Phone = @Phone,
            Address1 = @Address1,
            Address2 = @Address2,
            City = @City,
            State = @State,
            PostalCode = @PostalCode,
            Country = @Country,
            PrimaryContactName = @PrimaryContactName,
            PrimaryContactEmail = @PrimaryContactEmail,
            PrimaryContactPhone = @PrimaryContactPhone,
            PaymentTerms = @PaymentTerms,
            Notes = @Notes,
            IsActive = @IsActive,
            ModifiedBy = @ModifiedBy,
            ModifiedDate = GETDATE()
        WHERE VendorId = @VendorId;
    END
END
GO

-- =============================================
-- Purchase Order Stored Procedures
-- =============================================

-- Get all purchase orders
CREATE PROCEDURE sp_GetPurchaseOrders
AS
BEGIN
    SET NOCOUNT ON;
    SELECT po.*, v.VendorName, pot.TypeName as PurchaseOrderTypeName
    FROM PurchaseOrder po
    INNER JOIN Vendor v ON po.VendorId = v.VendorId
    INNER JOIN PurchaseOrderType pot ON po.PurchaseOrderTypeId = pot.PurchaseOrderTypeId
    WHERE po.IsActive = 1
    ORDER BY po.OrderDate DESC;
END
GO

-- Get purchase order by ID with items
CREATE PROCEDURE sp_GetPurchaseOrderById
    @PurchaseOrderId INT
AS
BEGIN
    SET NOCOUNT ON;
    -- Get purchase order header
    SELECT po.*, v.VendorName, pot.TypeName as PurchaseOrderTypeName
    FROM PurchaseOrder po
    INNER JOIN Vendor v ON po.VendorId = v.VendorId
    INNER JOIN PurchaseOrderType pot ON po.PurchaseOrderTypeId = pot.PurchaseOrderTypeId
    WHERE po.PurchaseOrderId = @PurchaseOrderId;
    
    -- Get purchase order items
    SELECT * FROM PurchaseOrderItem
    WHERE PurchaseOrderId = @PurchaseOrderId
    ORDER BY PurchaseOrderItemId;
END
GO

-- Insert or update purchase order
CREATE PROCEDURE sp_UpsertPurchaseOrder
    @PurchaseOrderId INT OUTPUT,
    @PurchaseOrderNumber NVARCHAR(20),
    @PurchaseOrderTypeId INT,
    @VendorId INT,
    @OrderDate DATETIME,
    @ExpectedDeliveryDate DATETIME = NULL,
    @ShippingAddress NVARCHAR(255) = NULL,
    @BillingAddress NVARCHAR(255) = NULL,
    @SubTotal DECIMAL(18,2) = 0,
    @TaxAmount DECIMAL(18,2) = 0,
    @ShippingAmount DECIMAL(18,2) = 0,
    @TotalAmount DECIMAL(18,2) = 0,
    @Currency NVARCHAR(3) = 'USD',
    @Status NVARCHAR(20) = 'Draft',
    @ApprovalDate DATETIME = NULL,
    @ApprovedBy NVARCHAR(50) = NULL,
    @Notes NVARCHAR(MAX) = NULL,
    @IsActive BIT = 1,
    @ModifiedBy NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @PurchaseOrderId = 0
    BEGIN
        -- Insert new purchase order
        INSERT INTO PurchaseOrder (
            PurchaseOrderNumber, PurchaseOrderTypeId, VendorId, OrderDate, ExpectedDeliveryDate,
            ShippingAddress, BillingAddress, SubTotal, TaxAmount, ShippingAmount, TotalAmount,
            Currency, Status, ApprovalDate, ApprovedBy, Notes, IsActive, CreatedBy, CreatedDate
        )
        VALUES (
            @PurchaseOrderNumber, @PurchaseOrderTypeId, @VendorId, @OrderDate, @ExpectedDeliveryDate,
            @ShippingAddress, @BillingAddress, @SubTotal, @TaxAmount, @ShippingAmount, @TotalAmount,
            @Currency, @Status, @ApprovalDate, @ApprovedBy, @Notes, @IsActive, @ModifiedBy, GETDATE()
        );
        
        SET @PurchaseOrderId = SCOPE_IDENTITY();
    END
    ELSE
    BEGIN
        -- Update existing purchase order
        UPDATE PurchaseOrder
        SET PurchaseOrderNumber = @PurchaseOrderNumber,
            PurchaseOrderTypeId = @PurchaseOrderTypeId,
            VendorId = @VendorId,
            OrderDate = @OrderDate,
            ExpectedDeliveryDate = @ExpectedDeliveryDate,
            ShippingAddress = @ShippingAddress,
            BillingAddress = @BillingAddress,
            SubTotal = @SubTotal,
            TaxAmount = @TaxAmount,
            ShippingAmount = @ShippingAmount,
            TotalAmount = @TotalAmount,
            Currency = @Currency,
            Status = @Status,
            ApprovalDate = @ApprovalDate,
            ApprovedBy = @ApprovedBy,
            Notes = @Notes,
            IsActive = @IsActive,
            ModifiedBy = @ModifiedBy,
            ModifiedDate = GETDATE()
        WHERE PurchaseOrderId = @PurchaseOrderId;
    END
END
GO

-- Insert purchase order item
CREATE PROCEDURE sp_InsertPurchaseOrderItem
    @PurchaseOrderItemId INT OUTPUT,
    @PurchaseOrderId INT,
    @ItemNumber NVARCHAR(50),
    @Description NVARCHAR(255),
    @Quantity DECIMAL(18,2),
    @UnitPrice DECIMAL(18,2),
    @UnitOfMeasure NVARCHAR(20) = NULL,
    @TaxRate DECIMAL(5,2) = NULL,
    @TaxAmount DECIMAL(18,2) = NULL,
    @TotalAmount DECIMAL(18,2),
    @Notes NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO PurchaseOrderItem (
        PurchaseOrderId, ItemNumber, Description, Quantity, UnitPrice,
        UnitOfMeasure, TaxRate, TaxAmount, TotalAmount, Notes, CreatedDate
    )
    VALUES (
        @PurchaseOrderId, @ItemNumber, @Description, @Quantity, @UnitPrice,
        @UnitOfMeasure, @TaxRate, @TaxAmount, @TotalAmount, @Notes, GETDATE()
    );
    
    SET @PurchaseOrderItemId = SCOPE_IDENTITY();
    
    -- Update purchase order totals
    UPDATE PurchaseOrder
    SET SubTotal = (SELECT SUM(TotalAmount) FROM PurchaseOrderItem WHERE PurchaseOrderId = @PurchaseOrderId),
        TaxAmount = (SELECT SUM(ISNULL(TaxAmount, 0)) FROM PurchaseOrderItem WHERE PurchaseOrderId = @PurchaseOrderId),
        TotalAmount = (SELECT SUM(TotalAmount) FROM PurchaseOrderItem WHERE PurchaseOrderId = @PurchaseOrderId) + 
                      (SELECT ISNULL(ShippingAmount, 0) FROM PurchaseOrder WHERE PurchaseOrderId = @PurchaseOrderId)
    WHERE PurchaseOrderId = @PurchaseOrderId;
END
GO

-- =============================================
-- Invoice Stored Procedures
-- =============================================

-- Get all invoices
CREATE PROCEDURE sp_GetInvoices
AS
BEGIN
    SET NOCOUNT ON;
    SELECT i.*, v.VendorName, ist.StatusName as InvoiceStatusName
    FROM Invoice i
    INNER JOIN Vendor v ON i.VendorId = v.VendorId
    INNER JOIN InvoiceStatus ist ON i.InvoiceStatusId = ist.InvoiceStatusId
    WHERE i.IsActive = 1
    ORDER BY i.InvoiceDate DESC;
END
GO

-- Get invoice by ID with items
CREATE PROCEDURE sp_GetInvoiceById
    @InvoiceId INT
AS
BEGIN
    SET NOCOUNT ON;
    -- Get invoice header
    SELECT i.*, v.VendorName, ist.StatusName as InvoiceStatusName,
           po.PurchaseOrderNumber
    FROM Invoice i
    INNER JOIN Vendor v ON i.VendorId = v.VendorId
    INNER JOIN InvoiceStatus ist ON i.InvoiceStatusId = ist.InvoiceStatusId
    LEFT JOIN PurchaseOrder po ON i.PurchaseOrderId = po.PurchaseOrderId
    WHERE i.InvoiceId = @InvoiceId;
    
    -- Get invoice items
    SELECT ii.*, poi.ItemNumber as PurchaseOrderItemNumber
    FROM InvoiceItem ii
    LEFT JOIN PurchaseOrderItem poi ON ii.PurchaseOrderItemId = poi.PurchaseOrderItemId
    WHERE ii.InvoiceId = @InvoiceId
    ORDER BY ii.InvoiceItemId;
END
GO

-- Insert or update invoice
CREATE PROCEDURE sp_UpsertInvoice
    @InvoiceId INT OUTPUT,
    @InvoiceNumber NVARCHAR(20),
    @VendorId INT,
    @PurchaseOrderId INT = NULL,
    @InvoiceStatusId INT,
    @InvoiceDate DATETIME,
    @DueDate DATETIME,
    @SubTotal DECIMAL(18,2) = 0,
    @TaxAmount DECIMAL(18,2) = 0,
    @ShippingAmount DECIMAL(18,2) = 0,
    @TotalAmount DECIMAL(18,2) = 0,
    @AmountPaid DECIMAL(18,2) = 0,
    @Currency NVARCHAR(3) = 'USD',
    @PaymentDate DATETIME = NULL,
    @PaymentReference NVARCHAR(50) = NULL,
    @Notes NVARCHAR(MAX) = NULL,
    @IsActive BIT = 1,
    @ModifiedBy NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @InvoiceId = 0
    BEGIN
        -- Insert new invoice
        INSERT INTO Invoice (
            InvoiceNumber, VendorId, PurchaseOrderId, InvoiceStatusId, InvoiceDate, DueDate,
            SubTotal, TaxAmount, ShippingAmount, TotalAmount, AmountPaid, Currency,
            PaymentDate, PaymentReference, Notes, IsActive, CreatedBy, CreatedDate
        )
        VALUES (
            @InvoiceNumber, @VendorId, @PurchaseOrderId, @InvoiceStatusId, @InvoiceDate, @DueDate,
            @SubTotal, @TaxAmount, @ShippingAmount, @TotalAmount, @AmountPaid, @Currency,
            @PaymentDate, @PaymentReference, @Notes, @IsActive, @ModifiedBy, GETDATE()
        );
        
        SET @InvoiceId = SCOPE_IDENTITY();
    END
    ELSE
    BEGIN
        -- Update existing invoice
        UPDATE Invoice
        SET InvoiceNumber = @InvoiceNumber,
            VendorId = @VendorId,
            PurchaseOrderId = @PurchaseOrderId,
            InvoiceStatusId = @InvoiceStatusId,
            InvoiceDate = @InvoiceDate,
            DueDate = @DueDate,
            SubTotal = @SubTotal,
            TaxAmount = @TaxAmount,
            ShippingAmount = @ShippingAmount,
            TotalAmount = @TotalAmount,
            AmountPaid = @AmountPaid,
            Currency = @Currency,
            PaymentDate = @PaymentDate,
            PaymentReference = @PaymentReference,
            Notes = @Notes,
            IsActive = @IsActive,
            ModifiedBy = @ModifiedBy,
            ModifiedDate = GETDATE()
        WHERE InvoiceId = @InvoiceId;
    END
END
GO

-- Insert invoice item
CREATE PROCEDURE sp_InsertInvoiceItem
    @InvoiceItemId INT OUTPUT,
    @InvoiceId INT,
    @PurchaseOrderItemId INT = NULL,
    @ItemNumber NVARCHAR(50),
    @Description NVARCHAR(255),
    @Quantity DECIMAL(18,2),
    @UnitPrice DECIMAL(18,2),
    @UnitOfMeasure NVARCHAR(20) = NULL,
    @TaxRate DECIMAL(5,2) = NULL,
    @TaxAmount DECIMAL(18,2) = NULL,
    @TotalAmount DECIMAL(18,2),
    @Notes NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO InvoiceItem (
        InvoiceId, PurchaseOrderItemId, ItemNumber, Description, Quantity, UnitPrice,
        UnitOfMeasure, TaxRate, TaxAmount, TotalAmount, Notes, CreatedDate
    )
    VALUES (
        @InvoiceId, @PurchaseOrderItemId, @ItemNumber, @Description, @Quantity, @UnitPrice,
        @UnitOfMeasure, @TaxRate, @TaxAmount, @TotalAmount, @Notes, GETDATE()
    );
    
    SET @InvoiceItemId = SCOPE_IDENTITY();
    
    -- Update invoice totals
    UPDATE Invoice
    SET SubTotal = (SELECT SUM(TotalAmount) FROM InvoiceItem WHERE InvoiceId = @InvoiceId),
        TaxAmount = (SELECT SUM(ISNULL(TaxAmount, 0)) FROM InvoiceItem WHERE InvoiceId = @InvoiceId),
        TotalAmount = (SELECT SUM(TotalAmount) FROM InvoiceItem WHERE InvoiceId = @InvoiceId) + 
                      (SELECT ISNULL(ShippingAmount, 0) FROM Invoice WHERE InvoiceId = @InvoiceId)
    WHERE InvoiceId = @InvoiceId;
END
GO

-- =============================================
-- Reference Data Stored Procedures
-- =============================================

-- Get all vendor types
CREATE PROCEDURE sp_GetVendorTypes
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM VendorType WHERE IsActive = 1 ORDER BY TypeName;
END
GO

-- Get all purchase order types
CREATE PROCEDURE sp_GetPurchaseOrderTypes
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM PurchaseOrderType WHERE IsActive = 1 ORDER BY TypeName;
END
GO

-- Get all invoice statuses
CREATE PROCEDURE sp_GetInvoiceStatuses
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM InvoiceStatus WHERE IsActive = 1 ORDER BY StatusName;
END
GO 