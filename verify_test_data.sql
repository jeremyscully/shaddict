-- =============================================
-- Test Data Verification Script for Transitus
-- =============================================

PRINT 'Verifying test data in Transitus database...';

-- Check domain data
PRINT '--- Domain Data ---';
PRINT 'VendorType count:';
SELECT COUNT(*) AS VendorTypeCount FROM VendorType;

PRINT 'PurchaseOrderType count:';
SELECT COUNT(*) AS PurchaseOrderTypeCount FROM PurchaseOrderType;

PRINT 'InvoiceStatus count:';
SELECT COUNT(*) AS InvoiceStatusCount FROM InvoiceStatus;

-- Check main tables
PRINT '--- Main Tables ---';
PRINT 'Vendor count:';
SELECT COUNT(*) AS VendorCount FROM Vendor;
PRINT 'Test vendors:';
SELECT VendorId, VendorName, VendorCode FROM Vendor WHERE VendorName LIKE 'Test%';

PRINT 'PurchaseOrder count:';
SELECT COUNT(*) AS PurchaseOrderCount FROM PurchaseOrder;
PRINT 'Test purchase orders:';
SELECT PurchaseOrderId, PurchaseOrderNumber, VendorId FROM PurchaseOrder WHERE PurchaseOrderNumber LIKE 'PO-TEST%';

PRINT 'PurchaseOrderItem count:';
SELECT COUNT(*) AS PurchaseOrderItemCount FROM PurchaseOrderItem;
PRINT 'Test purchase order items:';
SELECT PurchaseOrderItemId, PurchaseOrderId, ItemNumber, Description FROM PurchaseOrderItem WHERE Description LIKE '%';

PRINT 'Invoice count:';
SELECT COUNT(*) AS InvoiceCount FROM Invoice;
PRINT 'Test invoices:';
SELECT InvoiceId, InvoiceNumber, VendorId, PurchaseOrderId FROM Invoice WHERE InvoiceNumber LIKE 'INV-TEST%';

PRINT 'InvoiceItem count:';
SELECT COUNT(*) AS InvoiceItemCount FROM InvoiceItem;
PRINT 'Test invoice items:';
SELECT InvoiceItemId, InvoiceId, ItemNumber, Description FROM InvoiceItem WHERE Description LIKE '%';

-- Verify relationships
PRINT '--- Relationship Verification ---';
PRINT 'Purchase Orders with Vendor names:';
SELECT po.PurchaseOrderId, po.PurchaseOrderNumber, v.VendorName
FROM PurchaseOrder po
JOIN Vendor v ON po.VendorId = v.VendorId
WHERE po.PurchaseOrderNumber LIKE 'PO-TEST%';

PRINT 'Invoices with Vendor names and Purchase Order numbers:';
SELECT i.InvoiceId, i.InvoiceNumber, v.VendorName, po.PurchaseOrderNumber
FROM Invoice i
JOIN Vendor v ON i.VendorId = v.VendorId
JOIN PurchaseOrder po ON i.PurchaseOrderId = po.PurchaseOrderId
WHERE i.InvoiceNumber LIKE 'INV-TEST%';

PRINT 'Test data verification complete.'; 