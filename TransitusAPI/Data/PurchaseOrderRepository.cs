using Dapper;
using System.Data;
using TransitusAPI.Models;

namespace TransitusAPI.Data
{
    /// <summary>
    /// Repository for purchase order data operations
    /// </summary>
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private readonly DatabaseConnection _dbConnection;

        /// <summary>
        /// Initializes a new instance of the PurchaseOrderRepository class
        /// </summary>
        /// <param name="dbConnection">The database connection</param>
        public PurchaseOrderRepository(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        /// <summary>
        /// Gets all purchase orders
        /// </summary>
        /// <returns>A collection of purchase orders</returns>
        public async Task<IEnumerable<PurchaseOrder>> GetAllPurchaseOrdersAsync()
        {
            using var connection = _dbConnection.CreateConnection();
            return await connection.QueryAsync<PurchaseOrder>(
                "sp_GetPurchaseOrders", 
                commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Gets a purchase order by ID
        /// </summary>
        /// <param name="id">The purchase order ID</param>
        /// <returns>The purchase order if found, null otherwise</returns>
        public async Task<PurchaseOrder?> GetPurchaseOrderByIdAsync(int id)
        {
            using var connection = _dbConnection.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@PurchaseOrderId", id);
            
            return await connection.QueryFirstOrDefaultAsync<PurchaseOrder>(
                "sp_GetPurchaseOrderById", 
                parameters, 
                commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Gets purchase orders by vendor ID
        /// </summary>
        /// <param name="vendorId">The vendor ID</param>
        /// <returns>A collection of purchase orders for the specified vendor</returns>
        public async Task<IEnumerable<PurchaseOrder>> GetPurchaseOrdersByVendorIdAsync(int vendorId)
        {
            using var connection = _dbConnection.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@VendorId", vendorId);
            
            return await connection.QueryAsync<PurchaseOrder>(
                "sp_GetPurchaseOrdersByVendorId", 
                parameters, 
                commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Creates a new purchase order
        /// </summary>
        /// <param name="purchaseOrder">The purchase order to create</param>
        /// <returns>The created purchase order with ID</returns>
        public async Task<PurchaseOrder> CreatePurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            using var connection = _dbConnection.CreateConnection();
            var parameters = new DynamicParameters();
            
            // Set output parameter for the ID
            parameters.Add("@PurchaseOrderId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            
            // Add other parameters
            parameters.Add("@PurchaseOrderNumber", purchaseOrder.PONumber);
            parameters.Add("@PurchaseOrderTypeId", 1); // Default to Standard type
            parameters.Add("@VendorId", purchaseOrder.VendorId);
            parameters.Add("@OrderDate", purchaseOrder.OrderDate);
            parameters.Add("@ExpectedDeliveryDate", purchaseOrder.ExpectedDeliveryDate);
            parameters.Add("@ShippingAddress", ""); // Default empty
            parameters.Add("@BillingAddress", ""); // Default empty
            parameters.Add("@SubTotal", purchaseOrder.TotalAmount);
            parameters.Add("@TaxAmount", 0); // Default to 0
            parameters.Add("@ShippingAmount", 0); // Default to 0
            parameters.Add("@TotalAmount", purchaseOrder.TotalAmount);
            parameters.Add("@Currency", "USD"); // Default to USD
            parameters.Add("@Status", purchaseOrder.Status);
            parameters.Add("@ApprovalDate", null); // Default to null
            parameters.Add("@ApprovedBy", null); // Default to null
            parameters.Add("@Notes", purchaseOrder.Notes);
            parameters.Add("@IsActive", true);
            parameters.Add("@CreatedBy", purchaseOrder.CreatedBy ?? "System");
            
            await connection.ExecuteAsync(
                "sp_UpsertPurchaseOrder", 
                parameters, 
                commandType: CommandType.StoredProcedure);
            
            // Get the ID from the output parameter
            purchaseOrder.PurchaseOrderId = parameters.Get<int>("@PurchaseOrderId");
            
            return purchaseOrder;
        }

        /// <summary>
        /// Updates an existing purchase order
        /// </summary>
        /// <param name="purchaseOrder">The purchase order to update</param>
        /// <returns>True if updated, false otherwise</returns>
        public async Task<bool> UpdatePurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            using var connection = _dbConnection.CreateConnection();
            var parameters = new DynamicParameters();
            
            // Add parameters
            parameters.Add("@PurchaseOrderId", purchaseOrder.PurchaseOrderId, direction: ParameterDirection.InputOutput);
            parameters.Add("@PurchaseOrderNumber", purchaseOrder.PONumber);
            parameters.Add("@PurchaseOrderTypeId", 1); // Default to Standard type
            parameters.Add("@VendorId", purchaseOrder.VendorId);
            parameters.Add("@OrderDate", purchaseOrder.OrderDate);
            parameters.Add("@ExpectedDeliveryDate", purchaseOrder.ExpectedDeliveryDate);
            parameters.Add("@ShippingAddress", ""); // Default empty
            parameters.Add("@BillingAddress", ""); // Default empty
            parameters.Add("@SubTotal", purchaseOrder.TotalAmount);
            parameters.Add("@TaxAmount", 0); // Default to 0
            parameters.Add("@ShippingAmount", 0); // Default to 0
            parameters.Add("@TotalAmount", purchaseOrder.TotalAmount);
            parameters.Add("@Currency", "USD"); // Default to USD
            parameters.Add("@Status", purchaseOrder.Status);
            parameters.Add("@ApprovalDate", null); // Default to null
            parameters.Add("@ApprovedBy", null); // Default to null
            parameters.Add("@Notes", purchaseOrder.Notes);
            parameters.Add("@IsActive", true);
            parameters.Add("@CreatedBy", purchaseOrder.CreatedBy ?? "System");
            
            var result = await connection.ExecuteAsync(
                "sp_UpsertPurchaseOrder", 
                parameters, 
                commandType: CommandType.StoredProcedure);
            
            return result > 0;
        }

        /// <summary>
        /// Deletes a purchase order
        /// </summary>
        /// <param name="id">The purchase order ID to delete</param>
        /// <returns>True if deleted, false otherwise</returns>
        public async Task<bool> DeletePurchaseOrderAsync(int id)
        {
            using var connection = _dbConnection.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@PurchaseOrderId", id);
            
            var result = await connection.ExecuteAsync(
                "sp_DeletePurchaseOrder", 
                parameters, 
                commandType: CommandType.StoredProcedure);
            
            return result > 0;
        }
    }
} 