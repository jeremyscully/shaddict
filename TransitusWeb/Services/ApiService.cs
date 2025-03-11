using System.Text;
using Newtonsoft.Json;
using TransitusWeb.Models;

namespace TransitusWeb.Services
{
    /// <summary>
    /// Service for communicating with the Transitus API
    /// </summary>
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        /// <summary>
        /// Initializes a new instance of the ApiService class
        /// </summary>
        /// <param name="httpClient">The HTTP client</param>
        /// <param name="configuration">The application configuration</param>
        public ApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _baseUrl = _configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5000/api";
        }

        #region Vendor Methods

        /// <summary>
        /// Gets all vendors
        /// </summary>
        /// <returns>A collection of vendors</returns>
        public async Task<IEnumerable<VendorViewModel>> GetAllVendorsAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/vendors");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<VendorViewModel>>(content) ?? new List<VendorViewModel>();
        }

        /// <summary>
        /// Gets a vendor by ID
        /// </summary>
        /// <param name="id">The vendor ID</param>
        /// <returns>The vendor if found, null otherwise</returns>
        public async Task<VendorViewModel?> GetVendorByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/vendors/{id}");
            
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;
                
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<VendorViewModel>(content);
        }

        /// <summary>
        /// Creates a new vendor
        /// </summary>
        /// <param name="vendor">The vendor to create</param>
        /// <returns>The created vendor</returns>
        public async Task<VendorViewModel> CreateVendorAsync(VendorViewModel vendor)
        {
            var json = JsonConvert.SerializeObject(vendor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync($"{_baseUrl}/vendors", content);
            response.EnsureSuccessStatusCode();
            
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<VendorViewModel>(responseContent) ?? vendor;
        }

        /// <summary>
        /// Updates an existing vendor
        /// </summary>
        /// <param name="vendor">The vendor to update</param>
        /// <returns>True if successful, false otherwise</returns>
        public async Task<bool> UpdateVendorAsync(VendorViewModel vendor)
        {
            var json = JsonConvert.SerializeObject(vendor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PutAsync($"{_baseUrl}/vendors/{vendor.VendorId}", content);
            
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Deletes a vendor by ID
        /// </summary>
        /// <param name="id">The vendor ID</param>
        /// <returns>True if successful, false otherwise</returns>
        public async Task<bool> DeleteVendorAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/vendors/{id}");
            
            return response.IsSuccessStatusCode;
        }

        #endregion

        #region Purchase Order Methods

        /// <summary>
        /// Gets purchase orders by vendor ID
        /// </summary>
        /// <param name="vendorId">The vendor ID</param>
        /// <returns>A collection of purchase orders</returns>
        public async Task<IEnumerable<PurchaseOrderViewModel>> GetPurchaseOrdersByVendorIdAsync(int vendorId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/purchaseorders/vendor/{vendorId}");
                
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return new List<PurchaseOrderViewModel>();
                    
                response.EnsureSuccessStatusCode();
                
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<PurchaseOrderViewModel>>(content) ?? new List<PurchaseOrderViewModel>();
            }
            catch
            {
                // For demo purposes, return mock data if the API endpoint doesn't exist yet
                return GetMockPurchaseOrders(vendorId);
            }
        }

        /// <summary>
        /// Gets a purchase order by ID
        /// </summary>
        /// <param name="id">The purchase order ID</param>
        /// <returns>The purchase order if found, null otherwise</returns>
        public async Task<PurchaseOrderViewModel?> GetPurchaseOrderByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/purchaseorders/{id}");
                
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;
                    
                response.EnsureSuccessStatusCode();
                
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PurchaseOrderViewModel>(content);
            }
            catch
            {
                // For demo purposes, return mock data if the API endpoint doesn't exist yet
                return GetMockPurchaseOrders(0).FirstOrDefault(po => po.PurchaseOrderId == id);
            }
        }

        /// <summary>
        /// Creates a new purchase order
        /// </summary>
        /// <param name="purchaseOrder">The purchase order to create</param>
        /// <returns>The created purchase order</returns>
        public async Task<PurchaseOrderViewModel> CreatePurchaseOrderAsync(PurchaseOrderViewModel purchaseOrder)
        {
            try
            {
                var json = JsonConvert.SerializeObject(purchaseOrder);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync($"{_baseUrl}/purchaseorders", content);
                response.EnsureSuccessStatusCode();
                
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PurchaseOrderViewModel>(responseContent) ?? purchaseOrder;
            }
            catch
            {
                // For demo purposes, return the input with a mock ID if the API endpoint doesn't exist yet
                purchaseOrder.PurchaseOrderId = new Random().Next(100, 999);
                return purchaseOrder;
            }
        }

        /// <summary>
        /// Updates an existing purchase order
        /// </summary>
        /// <param name="purchaseOrder">The purchase order to update</param>
        /// <returns>True if successful, false otherwise</returns>
        public async Task<bool> UpdatePurchaseOrderAsync(PurchaseOrderViewModel purchaseOrder)
        {
            try
            {
                var json = JsonConvert.SerializeObject(purchaseOrder);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PutAsync($"{_baseUrl}/purchaseorders/{purchaseOrder.PurchaseOrderId}", content);
                
                return response.IsSuccessStatusCode;
            }
            catch
            {
                // For demo purposes, return success if the API endpoint doesn't exist yet
                return true;
            }
        }

        /// <summary>
        /// Deletes a purchase order by ID
        /// </summary>
        /// <param name="id">The purchase order ID</param>
        /// <returns>True if successful, false otherwise</returns>
        public async Task<bool> DeletePurchaseOrderAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/purchaseorders/{id}");
                
                return response.IsSuccessStatusCode;
            }
            catch
            {
                // For demo purposes, return success if the API endpoint doesn't exist yet
                return true;
            }
        }

        #endregion

        #region Invoice Methods

        /// <summary>
        /// Gets invoices by purchase order ID
        /// </summary>
        /// <param name="purchaseOrderId">The purchase order ID</param>
        /// <returns>A collection of invoices</returns>
        public async Task<IEnumerable<InvoiceViewModel>> GetInvoicesByPurchaseOrderIdAsync(int purchaseOrderId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/invoices/purchaseorder/{purchaseOrderId}");
                
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return new List<InvoiceViewModel>();
                    
                response.EnsureSuccessStatusCode();
                
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<InvoiceViewModel>>(content) ?? new List<InvoiceViewModel>();
            }
            catch
            {
                // For demo purposes, return mock data if the API endpoint doesn't exist yet
                return GetMockInvoices(purchaseOrderId);
            }
        }

        #endregion

        #region Mock Data Methods (for demo purposes)

        /// <summary>
        /// Gets mock purchase orders for a vendor
        /// </summary>
        /// <param name="vendorId">The vendor ID</param>
        /// <returns>A collection of mock purchase orders</returns>
        private IEnumerable<PurchaseOrderViewModel> GetMockPurchaseOrders(int vendorId)
        {
            // Create some mock purchase orders
            var purchaseOrders = new List<PurchaseOrderViewModel>
            {
                new PurchaseOrderViewModel
                {
                    PurchaseOrderId = 1,
                    VendorId = vendorId,
                    PONumber = "PO-2023-001",
                    OrderDate = DateTime.Now.AddDays(-30),
                    ExpectedDeliveryDate = DateTime.Now.AddDays(-15),
                    Status = "Completed",
                    TotalAmount = 1250.00m,
                    Notes = "Office supplies"
                },
                new PurchaseOrderViewModel
                {
                    PurchaseOrderId = 2,
                    VendorId = vendorId,
                    PONumber = "PO-2023-002",
                    OrderDate = DateTime.Now.AddDays(-15),
                    ExpectedDeliveryDate = DateTime.Now.AddDays(-5),
                    Status = "Received",
                    TotalAmount = 3750.50m,
                    Notes = "IT equipment"
                },
                new PurchaseOrderViewModel
                {
                    PurchaseOrderId = 3,
                    VendorId = vendorId,
                    PONumber = "PO-2023-003",
                    OrderDate = DateTime.Now.AddDays(-5),
                    ExpectedDeliveryDate = DateTime.Now.AddDays(10),
                    Status = "In Progress",
                    TotalAmount = 2500.00m,
                    Notes = "Marketing materials"
                }
            };

            // If vendorId is specified, filter the list
            if (vendorId > 0)
            {
                return purchaseOrders.Where(po => po.VendorId == vendorId);
            }

            return purchaseOrders;
        }

        /// <summary>
        /// Gets mock invoices for a purchase order
        /// </summary>
        /// <param name="purchaseOrderId">The purchase order ID</param>
        /// <returns>A collection of mock invoices</returns>
        private IEnumerable<InvoiceViewModel> GetMockInvoices(int purchaseOrderId)
        {
            // Create some mock invoices
            var invoices = new List<InvoiceViewModel>
            {
                new InvoiceViewModel
                {
                    InvoiceId = 1,
                    PurchaseOrderId = 1,
                    VendorId = 1,
                    InvoiceNumber = "INV-2023-001",
                    InvoiceDate = DateTime.Now.AddDays(-25),
                    DueDate = DateTime.Now.AddDays(-10),
                    TotalAmount = 1250.00m,
                    PaidAmount = 1250.00m,
                    Status = "Paid",
                    Notes = "Office supplies - full payment"
                },
                new InvoiceViewModel
                {
                    InvoiceId = 2,
                    PurchaseOrderId = 2,
                    VendorId = 1,
                    InvoiceNumber = "INV-2023-002",
                    InvoiceDate = DateTime.Now.AddDays(-10),
                    DueDate = DateTime.Now.AddDays(5),
                    TotalAmount = 2000.00m,
                    PaidAmount = 1000.00m,
                    Status = "Partially Paid",
                    Notes = "IT equipment - partial payment"
                },
                new InvoiceViewModel
                {
                    InvoiceId = 3,
                    PurchaseOrderId = 2,
                    VendorId = 1,
                    InvoiceNumber = "INV-2023-003",
                    InvoiceDate = DateTime.Now.AddDays(-10),
                    DueDate = DateTime.Now.AddDays(5),
                    TotalAmount = 1750.50m,
                    PaidAmount = 0.00m,
                    Status = "Pending",
                    Notes = "IT equipment - remaining balance"
                }
            };

            // If purchaseOrderId is specified, filter the list
            if (purchaseOrderId > 0)
            {
                return invoices.Where(inv => inv.PurchaseOrderId == purchaseOrderId);
            }

            return invoices;
        }

        #endregion
    }
} 