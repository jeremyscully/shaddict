-- =============================================
-- Check if tables exist in the database
-- =============================================

-- Check tables
SELECT 
    TABLE_NAME
FROM 
    INFORMATION_SCHEMA.TABLES
WHERE 
    TABLE_TYPE = 'BASE TABLE'
    AND TABLE_NAME IN (
        'Vendor',
        'PurchaseOrder',
        'PurchaseOrderItem',
        'Invoice',
        'InvoiceItem',
        'VendorType',
        'PurchaseOrderType',
        'InvoiceStatus'
    )
ORDER BY 
    TABLE_NAME;

-- Check Vendor table structure
PRINT 'Vendor Table Structure:';
EXEC sp_columns 'Vendor';

-- Check PurchaseOrder table structure
PRINT 'PurchaseOrder Table Structure:';
EXEC sp_columns 'PurchaseOrder';

-- Check PurchaseOrderItem table structure
PRINT 'PurchaseOrderItem Table Structure:';
EXEC sp_columns 'PurchaseOrderItem';

-- Check Invoice table structure
PRINT 'Invoice Table Structure:';
EXEC sp_columns 'Invoice';

-- Check InvoiceItem table structure
PRINT 'InvoiceItem Table Structure:';
EXEC sp_columns 'InvoiceItem'; 