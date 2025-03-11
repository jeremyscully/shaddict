namespace TransitusAPI.Models
{
    /// <summary>
    /// Represents an invoice in the Transitus system
    /// </summary>
    public class Invoice
    {
        /// <summary>
        /// The unique identifier for the invoice
        /// </summary>
        public int InvoiceId { get; set; }
        
        /// <summary>
        /// The vendor ID associated with this invoice
        /// </summary>
        public int VendorId { get; set; }
        
        /// <summary>
        /// The purchase order ID associated with this invoice (if any)
        /// </summary>
        public int? PurchaseOrderId { get; set; }
        
        /// <summary>
        /// The invoice number from the vendor
        /// </summary>
        public string InvoiceNumber { get; set; } = string.Empty;
        
        /// <summary>
        /// The date of the invoice
        /// </summary>
        public DateTime InvoiceDate { get; set; }
        
        /// <summary>
        /// The due date for payment
        /// </summary>
        public DateTime DueDate { get; set; }
        
        /// <summary>
        /// The total amount of the invoice
        /// </summary>
        public decimal TotalAmount { get; set; }
        
        /// <summary>
        /// The amount paid so far
        /// </summary>
        public decimal PaidAmount { get; set; } = 0;
        
        /// <summary>
        /// The status of the invoice (e.g., Pending, Approved, Paid, Rejected)
        /// </summary>
        public string Status { get; set; } = "Pending";
        
        /// <summary>
        /// Notes about the invoice
        /// </summary>
        public string? Notes { get; set; }
        
        /// <summary>
        /// The date the invoice was created in the system
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        /// <summary>
        /// The user who created the invoice record
        /// </summary>
        public string? CreatedBy { get; set; }
        
        /// <summary>
        /// The date the invoice was last modified
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        
        /// <summary>
        /// The user who last modified the invoice
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
} 