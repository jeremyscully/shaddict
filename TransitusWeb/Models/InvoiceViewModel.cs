using System.ComponentModel.DataAnnotations;

namespace TransitusWeb.Models
{
    /// <summary>
    /// View model for invoices
    /// </summary>
    public class InvoiceViewModel
    {
        /// <summary>
        /// Gets or sets the invoice ID
        /// </summary>
        public int InvoiceId { get; set; }

        /// <summary>
        /// Gets or sets the purchase order ID
        /// </summary>
        public int PurchaseOrderId { get; set; }

        /// <summary>
        /// Gets or sets the vendor ID
        /// </summary>
        public int VendorId { get; set; }

        /// <summary>
        /// Gets or sets the invoice number
        /// </summary>
        [Required(ErrorMessage = "Invoice Number is required")]
        [Display(Name = "Invoice Number")]
        [StringLength(50, ErrorMessage = "Invoice Number cannot exceed 50 characters")]
        public string InvoiceNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the invoice date
        /// </summary>
        [Required(ErrorMessage = "Invoice Date is required")]
        [Display(Name = "Invoice Date")]
        [DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the due date
        /// </summary>
        [Required(ErrorMessage = "Due Date is required")]
        [Display(Name = "Due Date")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; } = DateTime.Now.AddDays(30);

        /// <summary>
        /// Gets or sets the total amount
        /// </summary>
        [Required(ErrorMessage = "Total Amount is required")]
        [Display(Name = "Total Amount")]
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "Total Amount must be a positive number")]
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the paid amount
        /// </summary>
        [Display(Name = "Paid Amount")]
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "Paid Amount must be a positive number")]
        public decimal PaidAmount { get; set; }

        /// <summary>
        /// Gets or sets the status
        /// </summary>
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters")]
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the notes
        /// </summary>
        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        public string? Notes { get; set; }

        /// <summary>
        /// Gets the remaining balance to be paid
        /// </summary>
        [Display(Name = "Remaining Balance")]
        [DataType(DataType.Currency)]
        public decimal RemainingBalance => TotalAmount - PaidAmount;

        /// <summary>
        /// Gets whether the invoice is overdue
        /// </summary>
        [Display(Name = "Is Overdue")]
        public bool IsOverdue => DueDate < DateTime.Now && RemainingBalance > 0;
    }
} 