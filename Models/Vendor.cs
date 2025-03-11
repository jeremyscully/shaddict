using System;
using System.ComponentModel.DataAnnotations;

namespace TransitusWeb.Models
{
    /// <summary>
    /// Represents the status of a vendor in the system
    /// </summary>
    public enum VendorStatus
    {
        Active,
        Inactive,
        Pending,
        Blacklisted
    }

    /// <summary>
    /// Represents a vendor entity in the system
    /// </summary>
    public class Vendor
    {
        /// <summary>
        /// Unique identifier for the vendor
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the vendor company
        /// </summary>
        [Required(ErrorMessage = "Vendor name is required")]
        [StringLength(100, ErrorMessage = "Vendor name cannot exceed 100 characters")]
        [Display(Name = "Vendor Name")]
        public string Name { get; set; }

        /// <summary>
        /// Name of the primary contact person at the vendor
        /// </summary>
        [Required(ErrorMessage = "Contact name is required")]
        [StringLength(100, ErrorMessage = "Contact name cannot exceed 100 characters")]
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }

        /// <summary>
        /// Email address of the primary contact
        /// </summary>
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Phone number of the primary contact
        /// </summary>
        [Phone(ErrorMessage = "Invalid phone number")]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Current status of the vendor
        /// </summary>
        [Required(ErrorMessage = "Status is required")]
        [Display(Name = "Status")]
        public VendorStatus Status { get; set; }

        /// <summary>
        /// Category or type of products/services the vendor provides
        /// </summary>
        [Required(ErrorMessage = "Category is required")]
        [StringLength(50, ErrorMessage = "Category cannot exceed 50 characters")]
        [Display(Name = "Category")]
        public string Category { get; set; }

        /// <summary>
        /// Date when the vendor was added to the system
        /// </summary>
        [Display(Name = "Date Added")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateAdded { get; set; } = DateTime.Now;

        /// <summary>
        /// Optional notes about the vendor
        /// </summary>
        [Display(Name = "Notes")]
        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        public string Notes { get; set; }
    }
} 