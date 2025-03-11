using Dapper;
using System.Data;
using TransitusAPI.Models;

namespace TransitusAPI.Data
{
    /// <summary>
    /// Repository for vendor data operations
    /// </summary>
    public class VendorRepository : IVendorRepository
    {
        private readonly DatabaseConnection _dbConnection;

        /// <summary>
        /// Initializes a new instance of the VendorRepository class
        /// </summary>
        /// <param name="dbConnection">The database connection</param>
        public VendorRepository(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        /// <summary>
        /// Gets all vendors
        /// </summary>
        /// <returns>A collection of vendors</returns>
        public async Task<IEnumerable<Vendor>> GetAllVendorsAsync()
        {
            using var connection = _dbConnection.CreateConnection();
            // Call the stored procedure to get all vendors
            // Changed from sp_GetAllVendors to sp_GetVendors to match the actual stored procedure
            return await connection.QueryAsync<Vendor>("sp_GetVendors", commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Gets a vendor by ID
        /// </summary>
        /// <param name="id">The vendor ID</param>
        /// <returns>The vendor if found, null otherwise</returns>
        public async Task<Vendor?> GetVendorByIdAsync(int id)
        {
            using var connection = _dbConnection.CreateConnection();
            // Call the stored procedure to get a vendor by ID
            var parameters = new DynamicParameters();
            parameters.Add("@VendorId", id);
            
            return await connection.QueryFirstOrDefaultAsync<Vendor>(
                "sp_GetVendorById", 
                parameters, 
                commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Creates a new vendor
        /// </summary>
        /// <param name="vendor">The vendor to create</param>
        /// <returns>The ID of the created vendor</returns>
        public async Task<int> CreateVendorAsync(Vendor vendor)
        {
            using var connection = _dbConnection.CreateConnection();
            // Using sp_UpsertVendor with VendorId = 0 for new vendors
            var parameters = new DynamicParameters();
            parameters.Add("@VendorId", 0, DbType.Int32, ParameterDirection.InputOutput); // 0 means new vendor
            // Use default value 1 for VendorTypeId (assuming 1 is a valid type)
            parameters.Add("@VendorTypeId", 1);
            parameters.Add("@VendorName", vendor.Name);
            // Generate a simple vendor code if necessary
            parameters.Add("@VendorCode", $"V{DateTime.Now.ToString("yyyyMMdd")}");
            parameters.Add("@TaxId", vendor.TaxId);
            // Most fields are optional, map them from the simpler Vendor model
            parameters.Add("@Website", null);
            parameters.Add("@Email", vendor.Email);
            parameters.Add("@Phone", vendor.Phone);
            // Split the address field into address1, leaving other address fields null
            parameters.Add("@Address1", vendor.Address);
            parameters.Add("@Address2", null);
            parameters.Add("@City", null);
            parameters.Add("@State", null);
            parameters.Add("@PostalCode", null);
            parameters.Add("@Country", null);
            parameters.Add("@PrimaryContactName", vendor.ContactPerson);
            parameters.Add("@PrimaryContactEmail", vendor.Email);
            parameters.Add("@PrimaryContactPhone", vendor.Phone);
            parameters.Add("@PaymentTerms", null);
            parameters.Add("@Notes", vendor.Notes);
            parameters.Add("@IsActive", vendor.IsActive);
            parameters.Add("@ModifiedBy", "System");
            
            await connection.ExecuteAsync(
                "sp_UpsertVendor", 
                parameters, 
                commandType: CommandType.StoredProcedure);
            
            // Get the ID of the newly created vendor
            return parameters.Get<int>("@VendorId");
        }

        /// <summary>
        /// Updates an existing vendor
        /// </summary>
        /// <param name="vendor">The vendor to update</param>
        /// <returns>True if successful, false otherwise</returns>
        public async Task<bool> UpdateVendorAsync(Vendor vendor)
        {
            using var connection = _dbConnection.CreateConnection();
            // Using sp_UpsertVendor for updating existing vendors
            var parameters = new DynamicParameters();
            parameters.Add("@VendorId", vendor.VendorId, DbType.Int32, ParameterDirection.InputOutput);
            // Use default value 1 for VendorTypeId (assuming 1 is a valid type)
            parameters.Add("@VendorTypeId", 1);
            parameters.Add("@VendorName", vendor.Name);
            // Generate a vendor code if necessary
            parameters.Add("@VendorCode", $"V{DateTime.Now.ToString("yyyyMMdd")}");
            parameters.Add("@TaxId", vendor.TaxId);
            // Most fields are optional, map them from the simpler Vendor model
            parameters.Add("@Website", null);
            parameters.Add("@Email", vendor.Email);
            parameters.Add("@Phone", vendor.Phone);
            // Split the address field into address1, leaving other address fields null
            parameters.Add("@Address1", vendor.Address);
            parameters.Add("@Address2", null);
            parameters.Add("@City", null);
            parameters.Add("@State", null);
            parameters.Add("@PostalCode", null);
            parameters.Add("@Country", null);
            parameters.Add("@PrimaryContactName", vendor.ContactPerson);
            parameters.Add("@PrimaryContactEmail", vendor.Email);
            parameters.Add("@PrimaryContactPhone", vendor.Phone);
            parameters.Add("@PaymentTerms", null);
            parameters.Add("@Notes", vendor.Notes);
            parameters.Add("@IsActive", vendor.IsActive);
            parameters.Add("@ModifiedBy", "System");
            
            await connection.ExecuteAsync(
                "sp_UpsertVendor", 
                parameters, 
                commandType: CommandType.StoredProcedure);
            
            // If we get here, the update was successful
            return true;
        }

        /// <summary>
        /// Deletes a vendor by ID
        /// </summary>
        /// <param name="id">The vendor ID</param>
        /// <returns>True if successful, false otherwise</returns>
        public async Task<bool> DeleteVendorAsync(int id)
        {
            // Since there's no sp_DeleteVendor, we'll set IsActive to 0 (soft delete)
            using var connection = _dbConnection.CreateConnection();
            var vendor = await GetVendorByIdAsync(id);
            
            if (vendor == null)
                return false;
                
            vendor.IsActive = false;
            return await UpdateVendorAsync(vendor);
        }
    }
} 