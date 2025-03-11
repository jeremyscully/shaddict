namespace TransitusAPI.Models
{
    /// <summary>
    /// Represents a purchase order in the Transitus system
    /// </summary>
    public class PurchaseOrder
    {
        /// <summary>
        /// The unique identifier for the purchase order
        /// </summary>
        public int PurchaseOrderId { get; set; }
        
        /// <summary>
        /// The vendor ID associated with this purchase order
        /// </summary>
        public int VendorId { get; set; }
        
        /// <summary>
        /// The purchase order number
        /// </summary>
        public string PONumber { get; set; } = string.Empty;
        
        /// <summary>
        /// The date the purchase order was issued
        /// </summary>
        public DateTime OrderDate { get; set; }
        
        /// <summary>
        /// The expected delivery date
        /// </summary>
        public DateTime? ExpectedDeliveryDate { get; set; }
        
        /// <summary>
        /// The status of the purchase order (e.g., Draft, Submitted, Approved, Received)
        /// </summary>
        public string Status { get; set; } = "Draft";
        
        /// <summary>
        /// The total amount of the purchase order
        /// </summary>
        public decimal TotalAmount { get; set; }
        
        /// <summary>
        /// Notes about the purchase order
        /// </summary>
        public string? Notes { get; set; }
        
        /// <summary>
        /// The date the purchase order was created in the system
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        /// <summary>
        /// The user who created the purchase order
        /// </summary>
        public string? CreatedBy { get; set; }
        
        /// <summary>
        /// The date the purchase order was last modified
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        
        /// <summary>
        /// The user who last modified the purchase order
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
} 