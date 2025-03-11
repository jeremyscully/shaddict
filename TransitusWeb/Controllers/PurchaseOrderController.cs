using Microsoft.AspNetCore.Mvc;
using TransitusWeb.Models;
using TransitusWeb.Services;

namespace TransitusWeb.Controllers
{
    /// <summary>
    /// Controller for purchase order-related actions
    /// </summary>
    public class PurchaseOrdersController : Controller
    {
        private readonly ApiService _apiService;
        private readonly ILogger<PurchaseOrdersController> _logger;

        /// <summary>
        /// Initializes a new instance of the PurchaseOrderController class
        /// </summary>
        /// <param name="apiService">The API service</param>
        /// <param name="logger">The logger</param>
        public PurchaseOrdersController(ApiService apiService, ILogger<PurchaseOrdersController> logger)
        {
            _apiService = apiService;
            _logger = logger;
        }

        /// <summary>
        /// Displays a list of purchase orders for a specific vendor
        /// </summary>
        /// <param name="vendorId">The vendor ID</param>
        /// <returns>The view with the list of purchase orders</returns>
        public async Task<IActionResult> Index(int? vendorId)
        {
            try
            {
                if (vendorId == null)
                {
                    // If no vendor ID is specified, redirect to the vendors list
                    return RedirectToAction("Index", "Vendors");
                }

                var vendor = await _apiService.GetVendorByIdAsync(vendorId.Value);
                if (vendor == null)
                {
                    return NotFound();
                }

                ViewBag.VendorName = vendor.Name;
                ViewBag.VendorId = vendor.VendorId;

                var purchaseOrders = await _apiService.GetPurchaseOrdersByVendorIdAsync(vendorId.Value);
                
                // Set the vendor name for each purchase order
                foreach (var po in purchaseOrders)
                {
                    po.VendorName = vendor.Name;
                }
                
                return View(purchaseOrders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving purchase orders for vendor ID: {VendorId}", vendorId);
                TempData["ErrorMessage"] = "An error occurred while retrieving purchase orders. Please try again later.";
                return RedirectToAction("Index", "Vendors");
            }
        }

        /// <summary>
        /// Displays the details of a specific purchase order
        /// </summary>
        /// <param name="id">The purchase order ID</param>
        /// <returns>The view with the purchase order details</returns>
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var purchaseOrder = await _apiService.GetPurchaseOrderByIdAsync(id);
                
                if (purchaseOrder == null)
                {
                    return NotFound();
                }

                // Get the vendor information
                var vendor = await _apiService.GetVendorByIdAsync(purchaseOrder.VendorId);
                ViewBag.VendorName = vendor?.Name ?? "Unknown Vendor";
                purchaseOrder.VendorName = vendor?.Name ?? "Unknown Vendor";

                // Get invoices for this purchase order
                purchaseOrder.Invoices = (await _apiService.GetInvoicesByPurchaseOrderIdAsync(id)).ToList();

                return View(purchaseOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving purchase order details for ID: {PurchaseOrderId}", id);
                TempData["ErrorMessage"] = "An error occurred while retrieving purchase order details. Please try again later.";
                return RedirectToAction("Index", "Vendors");
            }
        }

        /// <summary>
        /// Displays the form to create a new purchase order
        /// </summary>
        /// <param name="vendorId">The vendor ID</param>
        /// <returns>The create purchase order form</returns>
        public async Task<IActionResult> Create(int vendorId)
        {
            try
            {
                var vendor = await _apiService.GetVendorByIdAsync(vendorId);
                if (vendor == null)
                {
                    return NotFound();
                }

                ViewBag.VendorName = vendor.Name;
                ViewBag.VendorId = vendorId;

                var purchaseOrder = new PurchaseOrderViewModel
                {
                    VendorId = vendorId,
                    VendorName = vendor.Name,
                    OrderDate = DateTime.Now,
                    Status = "Draft"
                };

                return View(purchaseOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error preparing create form for vendor ID: {VendorId}", vendorId);
                TempData["ErrorMessage"] = "An error occurred while preparing the create form. Please try again later.";
                return RedirectToAction("Index", "Vendors");
            }
        }

        /// <summary>
        /// Processes the creation of a new purchase order
        /// </summary>
        /// <param name="purchaseOrder">The purchase order to create</param>
        /// <returns>Redirects to the index page if successful, otherwise returns to the create form</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseOrderViewModel purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Implement the API call to create a purchase order
                    var createdPurchaseOrder = await _apiService.CreatePurchaseOrderAsync(purchaseOrder);
                    
                    TempData["SuccessMessage"] = "Purchase order created successfully.";
                    return RedirectToAction(nameof(Index), new { vendorId = purchaseOrder.VendorId });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating purchase order");
                    ModelState.AddModelError("", "An error occurred while creating the purchase order. Please try again.");
                }
            }

            // If we got this far, something failed, redisplay form
            try
            {
                var vendor = await _apiService.GetVendorByIdAsync(purchaseOrder.VendorId);
                ViewBag.VendorName = vendor?.Name ?? "Unknown Vendor";
                ViewBag.VendorId = purchaseOrder.VendorId;
                purchaseOrder.VendorName = vendor?.Name ?? "Unknown Vendor";
            }
            catch
            {
                // Ignore errors when retrieving vendor info for redisplay
            }

            return View(purchaseOrder);
        }

        /// <summary>
        /// Displays the form to edit an existing purchase order
        /// </summary>
        /// <param name="id">The purchase order ID</param>
        /// <returns>The edit purchase order form</returns>
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var purchaseOrder = await _apiService.GetPurchaseOrderByIdAsync(id);
                
                if (purchaseOrder == null)
                {
                    return NotFound();
                }

                var vendor = await _apiService.GetVendorByIdAsync(purchaseOrder.VendorId);
                ViewBag.VendorName = vendor?.Name ?? "Unknown Vendor";
                ViewBag.VendorId = purchaseOrder.VendorId;
                purchaseOrder.VendorName = vendor?.Name ?? "Unknown Vendor";

                return View(purchaseOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving purchase order for edit, ID: {PurchaseOrderId}", id);
                TempData["ErrorMessage"] = "An error occurred while retrieving the purchase order. Please try again later.";
                return RedirectToAction(nameof(Index), new { vendorId = 0 });
            }
        }

        /// <summary>
        /// Processes the update of an existing purchase order
        /// </summary>
        /// <param name="id">The purchase order ID</param>
        /// <param name="purchaseOrder">The updated purchase order</param>
        /// <returns>Redirects to the index page if successful, otherwise returns to the edit form</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PurchaseOrderViewModel purchaseOrder)
        {
            if (id != purchaseOrder.PurchaseOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Implement the API call to update a purchase order
                    var success = await _apiService.UpdatePurchaseOrderAsync(purchaseOrder);
                    
                    if (!success)
                    {
                        ModelState.AddModelError("", "Failed to update the purchase order. Please try again.");
                        return View(purchaseOrder);
                    }
                    
                    TempData["SuccessMessage"] = "Purchase order updated successfully.";
                    return RedirectToAction(nameof(Index), new { vendorId = purchaseOrder.VendorId });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating purchase order ID: {PurchaseOrderId}", id);
                    ModelState.AddModelError("", "An error occurred while updating the purchase order. Please try again.");
                }
            }

            // If we got this far, something failed, redisplay form
            try
            {
                var vendor = await _apiService.GetVendorByIdAsync(purchaseOrder.VendorId);
                ViewBag.VendorName = vendor?.Name ?? "Unknown Vendor";
                ViewBag.VendorId = purchaseOrder.VendorId;
                purchaseOrder.VendorName = vendor?.Name ?? "Unknown Vendor";
            }
            catch
            {
                // Ignore errors when retrieving vendor info for redisplay
            }

            return View(purchaseOrder);
        }

        /// <summary>
        /// Displays the confirmation page for deleting a purchase order
        /// </summary>
        /// <param name="id">The purchase order ID</param>
        /// <returns>The delete confirmation page</returns>
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var purchaseOrder = await _apiService.GetPurchaseOrderByIdAsync(id);
                
                if (purchaseOrder == null)
                {
                    return NotFound();
                }

                var vendor = await _apiService.GetVendorByIdAsync(purchaseOrder.VendorId);
                ViewBag.VendorName = vendor?.Name ?? "Unknown Vendor";
                purchaseOrder.VendorName = vendor?.Name ?? "Unknown Vendor";

                return View(purchaseOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving purchase order for delete, ID: {PurchaseOrderId}", id);
                TempData["ErrorMessage"] = "An error occurred while retrieving the purchase order. Please try again later.";
                return RedirectToAction(nameof(Index), new { vendorId = 0 });
            }
        }

        /// <summary>
        /// Processes the deletion of a purchase order
        /// </summary>
        /// <param name="id">The purchase order ID</param>
        /// <returns>Redirects to the index page</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var purchaseOrder = await _apiService.GetPurchaseOrderByIdAsync(id);
                if (purchaseOrder == null)
                {
                    return NotFound();
                }

                int vendorId = purchaseOrder.VendorId;

                // Implement the API call to delete a purchase order
                var success = await _apiService.DeletePurchaseOrderAsync(id);
                
                if (!success)
                {
                    TempData["ErrorMessage"] = "Failed to delete the purchase order. Please try again.";
                    return RedirectToAction(nameof(Delete), new { id });
                }
                
                TempData["SuccessMessage"] = "Purchase order deleted successfully.";
                return RedirectToAction(nameof(Index), new { vendorId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting purchase order ID: {PurchaseOrderId}", id);
                TempData["ErrorMessage"] = "An error occurred while deleting the purchase order. Please try again later.";
                return RedirectToAction(nameof(Index), new { vendorId = 0 });
            }
        }
    }
} 