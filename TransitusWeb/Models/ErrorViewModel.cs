namespace TransitusWeb.Models
{
    /// <summary>
    /// View model for error information
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// The request ID
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// Indicates if the request ID should be shown
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
} 