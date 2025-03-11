using TransitusAPI.Models;

namespace TransitusAPI.Data
{
    /// <summary>
    /// Interface for purchase order data access
    /// </summary>
    public interface IPurchaseOrderRepository
    {
        /// <summary>
        /// Get all purchase orders
        /// </summary>
        /// <returns>List of purchase orders</returns>
        Task<IEnumerable<PurchaseOrder>> GetAllPurchaseOrdersAsync();
        
        /// <summary>
        /// Get a purchase order by ID
        /// </summary>
        /// <param name="id">Purchase order ID</param>
        /// <returns>Purchase order if found, null otherwise</returns>
        Task<PurchaseOrder?> GetPurchaseOrderByIdAsync(int id);
        
        /// <summary>
        /// Get purchase orders by vendor ID
        /// </summary>
        /// <param name="vendorId">Vendor ID</param>
        /// <returns>List of purchase orders for the specified vendor</returns>
        Task<IEnumerable<PurchaseOrder>> GetPurchaseOrdersByVendorIdAsync(int vendorId);
        
        /// <summary>
        /// Create a new purchase order
        /// </summary>
        /// <param name="purchaseOrder">Purchase order to create</param>
        /// <returns>Created purchase order with ID</returns>
        Task<PurchaseOrder> CreatePurchaseOrderAsync(PurchaseOrder purchaseOrder);
        
        /// <summary>
        /// Update an existing purchase order
        /// </summary>
        /// <param name="purchaseOrder">Purchase order to update</param>
        /// <returns>True if updated, false otherwise</returns>
        Task<bool> UpdatePurchaseOrderAsync(PurchaseOrder purchaseOrder);
        
        /// <summary>
        /// Delete a purchase order
        /// </summary>
        /// <param name="id">Purchase order ID to delete</param>
        /// <returns>True if deleted, false otherwise</returns>
        Task<bool> DeletePurchaseOrderAsync(int id);
    }
} 