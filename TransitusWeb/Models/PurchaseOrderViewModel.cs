using System.ComponentModel.DataAnnotations;

namespace TransitusWeb.Models
{
    /// <summary>
    /// View model for purchase orders
    /// </summary>
    public class PurchaseOrderViewModel
    {
        /// <summary>
        /// Gets or sets the purchase order ID
        /// </summary>
        public int PurchaseOrderId { get; set; }

        /// <summary>
        /// Gets or sets the vendor ID
        /// </summary>
        public int VendorId { get; set; }

        /// <summary>
        /// Gets or sets the vendor name
        /// </summary>
        [Display(Name = "Vendor")]
        public string VendorName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the purchase order number
        /// </summary>
        [Required(ErrorMessage = "Purchase Order Number is required")]
        [Display(Name = "PO Number")]
        [StringLength(50, ErrorMessage = "PO Number cannot exceed 50 characters")]
        public string PONumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the order date
        /// </summary>
        [Required(ErrorMessage = "Order Date is required")]
        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the expected delivery date
        /// </summary>
        [Display(Name = "Expected Delivery Date")]
        [DataType(DataType.Date)]
        public DateTime? ExpectedDeliveryDate { get; set; }

        /// <summary>
        /// Gets or sets the status
        /// </summary>
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters")]
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the total amount
        /// </summary>
        [Display(Name = "Total Amount")]
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "Total Amount must be a positive number")]
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the notes
        /// </summary>
        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        public string? Notes { get; set; }

        /// <summary>
        /// Gets or sets the list of invoices associated with this purchase order
        /// </summary>
        public List<InvoiceViewModel> Invoices { get; set; } = new List<InvoiceViewModel>();
    }
} 