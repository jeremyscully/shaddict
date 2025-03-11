-- Insert data into VendorType table
INSERT INTO VendorType (TypeName, Description)
VALUES 
('Supplier', 'Companies that supply goods or raw materials'),
('Service Provider', 'Companies that provide services'),
('Contractor', 'Independent contractors or freelancers'),
('Consultant', 'Professional consultants'),
('Distributor', 'Companies that distribute products');

-- Insert data into PurchaseOrderType table
INSERT INTO PurchaseOrderType (TypeName, Description)
VALUES 
('Standard', 'Regular purchase order for goods or services'),
('Blanket', 'Long-term agreement with predetermined terms'),
('Contract', 'Purchase order tied to a specific contract'),
('Emergency', 'Urgent purchase orders with expedited processing'),
('Direct', 'Direct purchase without bidding process');

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