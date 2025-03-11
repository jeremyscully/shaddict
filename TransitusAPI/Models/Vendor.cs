namespace TransitusAPI.Models
{
    /// <summary>
    /// Represents a vendor in the Transitus system
    /// </summary>
    public class Vendor
    {
        /// <summary>
        /// The unique identifier for the vendor
        /// </summary>
        public int VendorId { get; set; }
        
        /// <summary>
        /// The name of the vendor
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// The vendor's contact person
        /// </summary>
        public string? ContactPerson { get; set; }
        
        /// <summary>
        /// The vendor's email address
        /// </summary>
        public string? Email { get; set; }
        
        /// <summary>
        /// The vendor's phone number
        /// </summary>
        public string? Phone { get; set; }
        
        /// <summary>
        /// The vendor's address
        /// </summary>
        public string? Address { get; set; }
        
        /// <summary>
        /// The vendor's tax identification number
        /// </summary>
        public string? TaxId { get; set; }
        
        /// <summary>
        /// Notes about the vendor
        /// </summary>
        public string? Notes { get; set; }
        
        /// <summary>
        /// Indicates if the vendor is active
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
} 