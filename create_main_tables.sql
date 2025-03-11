-- Create Vendor table
CREATE TABLE Vendor (
    VendorId INT PRIMARY KEY IDENTITY(1,1),
    VendorTypeId INT NOT NULL FOREIGN KEY REFERENCES VendorType(VendorTypeId),
    VendorName NVARCHAR(100) NOT NULL,
    VendorCode NVARCHAR(20) NOT NULL,
    TaxId NVARCHAR(20) NULL,
    Website NVARCHAR(255) NULL,
    Email NVARCHAR(100) NULL,
    Phone NVARCHAR(20) NULL,
    Address1 NVARCHAR(100) NULL,
    Address2 NVARCHAR(100) NULL,
    City NVARCHAR(50) NULL,
    State NVARCHAR(50) NULL,
    PostalCode NVARCHAR(20) NULL,
    Country NVARCHAR(50) NULL,
    PrimaryContactName NVARCHAR(100) NULL,
    PrimaryContactEmail NVARCHAR(100) NULL,
    PrimaryContactPhone NVARCHAR(20) NULL,
    PaymentTerms NVARCHAR(50) NULL,
    Notes NVARCHAR(MAX) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(50) NULL,
    ModifiedDate DATETIME NULL,
    ModifiedBy NVARCHAR(50) NULL
);

-- Create PurchaseOrder table
CREATE TABLE PurchaseOrder (
    PurchaseOrderId INT PRIMARY KEY IDENTITY(1,1),
    PurchaseOrderNumber NVARCHAR(20) NOT NULL,
    PurchaseOrderTypeId INT NOT NULL FOREIGN KEY REFERENCES PurchaseOrderType(PurchaseOrderTypeId),
    VendorId INT NOT NULL FOREIGN KEY REFERENCES Vendor(VendorId),
    OrderDate DATETIME NOT NULL,
    ExpectedDeliveryDate DATETIME NULL,
    ShippingAddress NVARCHAR(255) NULL,
    BillingAddress NVARCHAR(255) NULL,
    SubTotal DECIMAL(18,2) NOT NULL DEFAULT 0,
    TaxAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    ShippingAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    TotalAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    Currency NVARCHAR(3) NOT NULL DEFAULT 'USD',
    Status NVARCHAR(20) NOT NULL DEFAULT 'Draft', -- Draft, Submitted, Approved, Rejected, Completed, Cancelled
    ApprovalDate DATETIME NULL,
    ApprovedBy NVARCHAR(50) NULL,
    Notes NVARCHAR(MAX) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(50) NULL,
    ModifiedDate DATETIME NULL,
    ModifiedBy NVARCHAR(50) NULL
);

-- Create PurchaseOrderItem table
CREATE TABLE PurchaseOrderItem (
    PurchaseOrderItemId INT PRIMARY KEY IDENTITY(1,1),
    PurchaseOrderId INT NOT NULL FOREIGN KEY REFERENCES PurchaseOrder(PurchaseOrderId),
    ItemNumber NVARCHAR(50) NOT NULL,
    Description NVARCHAR(255) NOT NULL,
    Quantity DECIMAL(18,2) NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    UnitOfMeasure NVARCHAR(20) NULL,
    TaxRate DECIMAL(5,2) NULL,
    TaxAmount DECIMAL(18,2) NULL,
    TotalAmount DECIMAL(18,2) NOT NULL,
    Notes NVARCHAR(MAX) NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME NULL
);

-- Create Invoice table
CREATE TABLE Invoice (
    InvoiceId INT PRIMARY KEY IDENTITY(1,1),
    InvoiceNumber NVARCHAR(20) NOT NULL,
    VendorId INT NOT NULL FOREIGN KEY REFERENCES Vendor(VendorId),
    PurchaseOrderId INT NULL FOREIGN KEY REFERENCES PurchaseOrder(PurchaseOrderId),
    InvoiceStatusId INT NOT NULL FOREIGN KEY REFERENCES InvoiceStatus(InvoiceStatusId),
    InvoiceDate DATETIME NOT NULL,
    DueDate DATETIME NOT NULL,
    SubTotal DECIMAL(18,2) NOT NULL DEFAULT 0,
    TaxAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    ShippingAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    TotalAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    AmountPaid DECIMAL(18,2) NOT NULL DEFAULT 0,
    Currency NVARCHAR(3) NOT NULL DEFAULT 'USD',
    PaymentDate DATETIME NULL,
    PaymentReference NVARCHAR(50) NULL,
    Notes NVARCHAR(MAX) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(50) NULL,
    ModifiedDate DATETIME NULL,
    ModifiedBy NVARCHAR(50) NULL
);

-- Create InvoiceItem table
CREATE TABLE InvoiceItem (
    InvoiceItemId INT PRIMARY KEY IDENTITY(1,1),
    InvoiceId INT NOT NULL FOREIGN KEY REFERENCES Invoice(InvoiceId),
    PurchaseOrderItemId INT NULL FOREIGN KEY REFERENCES PurchaseOrderItem(PurchaseOrderItemId),
    ItemNumber NVARCHAR(50) NOT NULL,
    Description NVARCHAR(255) NOT NULL,
    Quantity DECIMAL(18,2) NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    UnitOfMeasure NVARCHAR(20) NULL,
    TaxRate DECIMAL(5,2) NULL,
    TaxAmount DECIMAL(18,2) NULL,
    TotalAmount DECIMAL(18,2) NOT NULL,
    Notes NVARCHAR(MAX) NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ModifiedDate DATETIME NULL
); 