using System.ComponentModel.DataAnnotations;

namespace TransitusWeb.Models
{
    /// <summary>
    /// View model for vendor data
    /// </summary>
    public class VendorViewModel
    {
        /// <summary>
        /// The unique identifier for the vendor
        /// </summary>
        public int VendorId { get; set; }
        
        /// <summary>
        /// The name of the vendor
        /// </summary>
        [Required(ErrorMessage = "Vendor name is required")]
        [Display(Name = "Vendor Name")]
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// The vendor's contact person
        /// </summary>
        [Display(Name = "Contact Person")]
        public string? ContactPerson { get; set; }
        
        /// <summary>
        /// The vendor's contact name (alias for ContactPerson)
        /// </summary>
        [Display(Name = "Contact Name")]
        public string? ContactName { 
            get => ContactPerson; 
            set => ContactPerson = value; 
        }
        
        /// <summary>
        /// The vendor's email address
        /// </summary>
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Display(Name = "Email")]
        public string? Email { get; set; }
        
        /// <summary>
        /// The vendor's phone number
        /// </summary>
        [Phone(ErrorMessage = "Invalid phone number")]
        [Display(Name = "Phone")]
        public string? Phone { get; set; }
        
        /// <summary>
        /// The vendor's address
        /// </summary>
        [Display(Name = "Address")]
        public string? Address { get; set; }
        
        /// <summary>
        /// The vendor's city
        /// </summary>
        [Display(Name = "City")]
        public string? City { get; set; }
        
        /// <summary>
        /// The vendor's state or province
        /// </summary>
        [Display(Name = "State")]
        public string? State { get; set; }
        
        /// <summary>
        /// The vendor's zip or postal code
        /// </summary>
        [Display(Name = "Zip Code")]
        public string? ZipCode { get; set; }
        
        /// <summary>
        /// The vendor's tax identification number
        /// </summary>
        [Display(Name = "Tax ID")]
        public string? TaxId { get; set; }
        
        /// <summary>
        /// Notes about the vendor
        /// </summary>
        [Display(Name = "Notes")]
        public string? Notes { get; set; }
        
        /// <summary>
        /// Indicates if the vendor is active
        /// </summary>
        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;
        
        /// <summary>
        /// List of purchase orders associated with this vendor
        /// </summary>
        public List<PurchaseOrderViewModel> PurchaseOrders { get; set; } = new List<PurchaseOrderViewModel>();
    }
} 