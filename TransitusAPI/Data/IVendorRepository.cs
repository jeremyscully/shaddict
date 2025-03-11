using TransitusAPI.Models;

namespace TransitusAPI.Data
{
    /// <summary>
    /// Interface for vendor data operations
    /// </summary>
    public interface IVendorRepository
    {
        /// <summary>
        /// Gets all vendors
        /// </summary>
        /// <returns>A collection of vendors</returns>
        Task<IEnumerable<Vendor>> GetAllVendorsAsync();
        
        /// <summary>
        /// Gets a vendor by ID
        /// </summary>
        /// <param name="id">The vendor ID</param>
        /// <returns>The vendor if found, null otherwise</returns>
        Task<Vendor?> GetVendorByIdAsync(int id);
        
        /// <summary>
        /// Creates a new vendor
        /// </summary>
        /// <param name="vendor">The vendor to create</param>
        /// <returns>The ID of the created vendor</returns>
        Task<int> CreateVendorAsync(Vendor vendor);
        
        /// <summary>
        /// Updates an existing vendor
        /// </summary>
        /// <param name="vendor">The vendor to update</param>
        /// <returns>True if successful, false otherwise</returns>
        Task<bool> UpdateVendorAsync(Vendor vendor);
        
        /// <summary>
        /// Deletes a vendor by ID
        /// </summary>
        /// <param name="id">The vendor ID</param>
        /// <returns>True if successful, false otherwise</returns>
        Task<bool> DeleteVendorAsync(int id);
    }
} 