-- =============================================
-- Check stored procedure definition
-- =============================================

-- Check sp_UpsertVendor definition
PRINT 'sp_UpsertVendor Definition:';
SELECT OBJECT_DEFINITION(OBJECT_ID('sp_UpsertVendor')) AS Definition; 