using Microsoft.AspNetCore.Mvc;
using TransitusWeb.Models;
using TransitusWeb.Services;

namespace TransitusWeb.Controllers
{
    /// <summary>
    /// Controller for vendor operations
    /// </summary>
    public class VendorsController : Controller
    {
        private readonly ApiService _apiService;
        private readonly ILogger<VendorsController> _logger;

        /// <summary>
        /// Initializes a new instance of the VendorsController class
        /// </summary>
        /// <param name="apiService">The API service</param>
        /// <param name="logger">The logger</param>
        public VendorsController(ApiService apiService, ILogger<VendorsController> logger)
        {
            _apiService = apiService;
            _logger = logger;
        }

        /// <summary>
        /// Displays a list of all vendors
        /// </summary>
        /// <returns>The index view</returns>
        public async Task<IActionResult> Index()
        {
            try
            {
                var vendors = await _apiService.GetAllVendorsAsync();
                return View(vendors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving vendors");
                TempData["ErrorMessage"] = "Error retrieving vendors. Please try again later.";
                return View(new List<VendorViewModel>());
            }
        }

        /// <summary>
        /// Displays details for a specific vendor
        /// </summary>
        /// <param name="id">The vendor ID</param>
        /// <returns>The details view</returns>
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var vendor = await _apiService.GetVendorByIdAsync(id);
                
                if (vendor == null)
                {
                    _logger.LogWarning("Vendor with ID {VendorId} not found", id);
                    return NotFound();
                }
                
                return View(vendor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving vendor with ID {VendorId}", id);
                TempData["ErrorMessage"] = "Error retrieving vendor details. Please try again later.";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// Displays the create vendor form
        /// </summary>
        /// <returns>The create view</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Processes the create vendor form submission
        /// </summary>
        /// <param name="vendor">The vendor to create</param>
        /// <returns>Redirects to the index view if successful</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VendorViewModel vendor)
        {
            if (!ModelState.IsValid)
            {
                return View(vendor);
            }
            
            try
            {
                await _apiService.CreateVendorAsync(vendor);
                TempData["SuccessMessage"] = "Vendor created successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating vendor");
                ModelState.AddModelError("", "Error creating vendor. Please try again later.");
                return View(vendor);
            }
        }

        /// <summary>
        /// Displays the edit vendor form
        /// </summary>
        /// <param name="id">The vendor ID</param>
        /// <returns>The edit view</returns>
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var vendor = await _apiService.GetVendorByIdAsync(id);
                
                if (vendor == null)
                {
                    _logger.LogWarning("Vendor with ID {VendorId} not found", id);
                    return NotFound();
                }
                
                return View(vendor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving vendor with ID {VendorId} for editing", id);
                TempData["ErrorMessage"] = "Error retrieving vendor for editing. Please try again later.";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// Processes the edit vendor form submission
        /// </summary>
        /// <param name="id">The vendor ID</param>
        /// <param name="vendor">The updated vendor data</param>
        /// <returns>Redirects to the index view if successful</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VendorViewModel vendor)
        {
            if (id != vendor.VendorId)
            {
                _logger.LogWarning("ID mismatch: URL ID {UrlId} doesn't match vendor ID {VendorId}", id, vendor.VendorId);
                return NotFound();
            }
            
            if (!ModelState.IsValid)
            {
                return View(vendor);
            }
            
            try
            {
                var result = await _apiService.UpdateVendorAsync(vendor);
                
                if (!result)
                {
                    _logger.LogWarning("Failed to update vendor with ID {VendorId}", id);
                    ModelState.AddModelError("", "Error updating vendor. Please try again later.");
                    return View(vendor);
                }
                
                TempData["SuccessMessage"] = "Vendor updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating vendor with ID {VendorId}", id);
                ModelState.AddModelError("", "Error updating vendor. Please try again later.");
                return View(vendor);
            }
        }

        /// <summary>
        /// Displays the delete vendor confirmation
        /// </summary>
        /// <param name="id">The vendor ID</param>
        /// <returns>The delete view</returns>
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var vendor = await _apiService.GetVendorByIdAsync(id);
                
                if (vendor == null)
                {
                    _logger.LogWarning("Vendor with ID {VendorId} not found", id);
                    return NotFound();
                }
                
                return View(vendor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving vendor with ID {VendorId} for deletion", id);
                TempData["ErrorMessage"] = "Error retrieving vendor for deletion. Please try again later.";
                return RedirectToAction(nameof(Index));
            }
        }

        /// <summary>
        /// Processes the delete vendor confirmation
        /// </summary>
        /// <param name="id">The vendor ID</param>
        /// <returns>Redirects to the index view if successful</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _apiService.DeleteVendorAsync(id);
                
                if (!result)
                {
                    _logger.LogWarning("Failed to delete vendor with ID {VendorId}", id);
                    TempData["ErrorMessage"] = "Error deleting vendor. Please try again later.";
                    return RedirectToAction(nameof(Index));
                }
                
                TempData["SuccessMessage"] = "Vendor deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting vendor with ID {VendorId}", id);
                TempData["ErrorMessage"] = "Error deleting vendor. Please try again later.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
} 