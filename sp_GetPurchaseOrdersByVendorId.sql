-- =============================================
-- Get purchase orders by vendor ID
-- =============================================
CREATE PROCEDURE sp_GetPurchaseOrdersByVendorId
    @VendorId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Get purchase order headers for the specified vendor
    SELECT po.*, v.VendorName, pot.TypeName as PurchaseOrderTypeName
    FROM PurchaseOrder po
    INNER JOIN Vendor v ON po.VendorId = v.VendorId
    INNER JOIN PurchaseOrderType pot ON po.PurchaseOrderTypeId = pot.PurchaseOrderTypeId
    WHERE po.VendorId = @VendorId AND po.IsActive = 1
    ORDER BY po.OrderDate DESC;
    
    -- Get purchase order items for all returned purchase orders
    SELECT poi.*
    FROM PurchaseOrderItem poi
    INNER JOIN PurchaseOrder po ON poi.PurchaseOrderId = po.PurchaseOrderId
    WHERE po.VendorId = @VendorId AND po.IsActive = 1
    ORDER BY poi.PurchaseOrderId, poi.PurchaseOrderItemId;
END
GO 