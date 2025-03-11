-- =============================================
-- Check stored procedure parameters
-- =============================================

-- Check sp_UpsertVendor parameters
PRINT 'sp_UpsertVendor Parameters:';
SELECT 
    p.name AS ParameterName,
    t.name AS DataType,
    p.max_length AS MaxLength,
    p.is_output AS IsOutput
FROM 
    sys.parameters p
    INNER JOIN sys.procedures sp ON p.object_id = sp.object_id
    INNER JOIN sys.types t ON p.system_type_id = t.system_type_id
WHERE 
    sp.name = 'sp_UpsertVendor'
ORDER BY 
    p.parameter_id;

-- Check sp_UpsertPurchaseOrder parameters
PRINT 'sp_UpsertPurchaseOrder Parameters:';
SELECT 
    p.name AS ParameterName,
    t.name AS DataType,
    p.max_length AS MaxLength,
    p.is_output AS IsOutput
FROM 
    sys.parameters p
    INNER JOIN sys.procedures sp ON p.object_id = sp.object_id
    INNER JOIN sys.types t ON p.system_type_id = t.system_type_id
WHERE 
    sp.name = 'sp_UpsertPurchaseOrder'
ORDER BY 
    p.parameter_id;

-- Check sp_InsertPurchaseOrderItem parameters
PRINT 'sp_InsertPurchaseOrderItem Parameters:';
SELECT 
    p.name AS ParameterName,
    t.name AS DataType,
    p.max_length AS MaxLength,
    p.is_output AS IsOutput
FROM 
    sys.parameters p
    INNER JOIN sys.procedures sp ON p.object_id = sp.object_id
    INNER JOIN sys.types t ON p.system_type_id = t.system_type_id
WHERE 
    sp.name = 'sp_InsertPurchaseOrderItem'
ORDER BY 
    p.parameter_id;

-- Check sp_UpsertInvoice parameters
PRINT 'sp_UpsertInvoice Parameters:';
SELECT 
    p.name AS ParameterName,
    t.name AS DataType,
    p.max_length AS MaxLength,
    p.is_output AS IsOutput
FROM 
    sys.parameters p
    INNER JOIN sys.procedures sp ON p.object_id = sp.object_id
    INNER JOIN sys.types t ON p.system_type_id = t.system_type_id
WHERE 
    sp.name = 'sp_UpsertInvoice'
ORDER BY 
    p.parameter_id;

-- Check sp_InsertInvoiceItem parameters
PRINT 'sp_InsertInvoiceItem Parameters:';
SELECT 
    p.name AS ParameterName,
    t.name AS DataType,
    p.max_length AS MaxLength,
    p.is_output AS IsOutput
FROM 
    sys.parameters p
    INNER JOIN sys.procedures sp ON p.object_id = sp.object_id
    INNER JOIN sys.types t ON p.system_type_id = t.system_type_id
WHERE 
    sp.name = 'sp_InsertInvoiceItem'
ORDER BY 
    p.parameter_id; 