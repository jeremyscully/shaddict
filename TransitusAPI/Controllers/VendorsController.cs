using Microsoft.AspNetCore.Mvc;
using TransitusAPI.Data;
using TransitusAPI.Models;

namespace TransitusAPI.Controllers
{
    /// <summary>
    /// API controller for vendor operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class VendorsController : ControllerBase
    {
        private readonly IVendorRepository _vendorRepository;
        private readonly ILogger<VendorsController> _logger;

        /// <summary>
        /// Initializes a new instance of the VendorsController class
        /// </summary>
        /// <param name="vendorRepository">The vendor repository</param>
        /// <param name="logger">The logger</param>
        public VendorsController(IVendorRepository vendorRepository, ILogger<VendorsController> logger)
        {
            _vendorRepository = vendorRepository;
            _logger = logger;
        }

        /// <summary>
        /// Gets all vendors
        /// </summary>
        /// <returns>A collection of vendors</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetVendors()
        {
            try
            {
                _logger.LogInformation("Getting all vendors");
                var vendors = await _vendorRepository.GetAllVendorsAsync();
                return Ok(vendors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all vendors");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        /// <summary>
        /// Gets a vendor by ID
        /// </summary>
        /// <param name="id">The vendor ID</param>
        /// <returns>The vendor if found</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Vendor>> GetVendor(int id)
        {
            try
            {
                _logger.LogInformation("Getting vendor with ID: {VendorId}", id);
                var vendor = await _vendorRepository.GetVendorByIdAsync(id);
                
                if (vendor == null)
                {
                    _logger.LogWarning("Vendor with ID: {VendorId} not found", id);
                    return NotFound($"Vendor with ID {id} not found");
                }
                
                return Ok(vendor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting vendor with ID: {VendorId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        /// <summary>
        /// Creates a new vendor
        /// </summary>
        /// <param name="vendor">The vendor to create</param>
        /// <returns>The created vendor</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Vendor>> CreateVendor(Vendor vendor)
        {
            try
            {
                if (vendor == null)
                {
                    _logger.LogWarning("Vendor object is null");
                    return BadRequest("Vendor object is null");
                }
                
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid vendor model state");
                    return BadRequest("Invalid model object");
                }
                
                _logger.LogInformation("Creating new vendor: {VendorName}", vendor.Name);
                var id = await _vendorRepository.CreateVendorAsync(vendor);
                vendor.VendorId = id;
                
                return CreatedAtAction(nameof(GetVendor), new { id = vendor.VendorId }, vendor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating vendor: {VendorName}", vendor?.Name);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new vendor");
            }
        }

        /// <summary>
        /// Updates an existing vendor
        /// </summary>
        /// <param name="id">The vendor ID</param>
        /// <param name="vendor">The updated vendor data</param>
        /// <returns>No content if successful</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateVendor(int id, Vendor vendor)
        {
            try
            {
                if (vendor == null)
                {
                    _logger.LogWarning("Vendor object is null");
                    return BadRequest("Vendor object is null");
                }
                
                if (id != vendor.VendorId)
                {
                    _logger.LogWarning("ID mismatch: URL ID {UrlId} doesn't match vendor ID {VendorId}", id, vendor.VendorId);
                    return BadRequest("ID mismatch");
                }
                
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid vendor model state");
                    return BadRequest("Invalid model object");
                }
                
                // Check if vendor exists
                var existingVendor = await _vendorRepository.GetVendorByIdAsync(id);
                if (existingVendor == null)
                {
                    _logger.LogWarning("Vendor with ID: {VendorId} not found", id);
                    return NotFound($"Vendor with ID {id} not found");
                }
                
                _logger.LogInformation("Updating vendor with ID: {VendorId}", id);
                var result = await _vendorRepository.UpdateVendorAsync(vendor);
                
                if (!result)
                {
                    _logger.LogWarning("Failed to update vendor with ID: {VendorId}", id);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error updating vendor");
                }
                
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating vendor with ID: {VendorId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating vendor");
            }
        }

        /// <summary>
        /// Deletes a vendor by ID
        /// </summary>
        /// <param name="id">The vendor ID</param>
        /// <returns>No content if successful</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteVendor(int id)
        {
            try
            {
                // Check if vendor exists
                var vendor = await _vendorRepository.GetVendorByIdAsync(id);
                if (vendor == null)
                {
                    _logger.LogWarning("Vendor with ID: {VendorId} not found", id);
                    return NotFound($"Vendor with ID {id} not found");
                }
                
                _logger.LogInformation("Deleting vendor with ID: {VendorId}", id);
                var result = await _vendorRepository.DeleteVendorAsync(id);
                
                if (!result)
                {
                    _logger.LogWarning("Failed to delete vendor with ID: {VendorId}", id);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting vendor");
                }
                
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting vendor with ID: {VendorId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting vendor");
            }
        }
    }
} 