-- =============================================
-- Check if stored procedures exist in the database
-- =============================================

SELECT 
    ROUTINE_NAME
FROM 
    INFORMATION_SCHEMA.ROUTINES
WHERE 
    ROUTINE_TYPE = 'PROCEDURE'
    AND ROUTINE_NAME IN (
        'sp_UpsertVendor',
        'sp_UpsertPurchaseOrder',
        'sp_InsertPurchaseOrderItem',
        'sp_UpsertInvoice',
        'sp_InsertInvoiceItem'
    )
ORDER BY 
    ROUTINE_NAME; 