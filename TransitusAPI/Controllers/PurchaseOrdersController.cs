using Microsoft.AspNetCore.Mvc;
using TransitusAPI.Data;
using TransitusAPI.Models;

namespace TransitusAPI.Controllers
{
    /// <summary>
    /// API controller for purchase order operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly ILogger<PurchaseOrdersController> _logger;

        /// <summary>
        /// Initializes a new instance of the PurchaseOrdersController class
        /// </summary>
        /// <param name="purchaseOrderRepository">The purchase order repository</param>
        /// <param name="logger">The logger</param>
        public PurchaseOrdersController(IPurchaseOrderRepository purchaseOrderRepository, ILogger<PurchaseOrdersController> logger)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
            _logger = logger;
        }

        /// <summary>
        /// Gets all purchase orders
        /// </summary>
        /// <returns>A collection of purchase orders</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PurchaseOrder>>> GetPurchaseOrders()
        {
            try
            {
                _logger.LogInformation("Getting all purchase orders");
                var purchaseOrders = await _purchaseOrderRepository.GetAllPurchaseOrdersAsync();
                return Ok(purchaseOrders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all purchase orders");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        /// <summary>
        /// Gets a purchase order by ID
        /// </summary>
        /// <param name="id">The purchase order ID</param>
        /// <returns>The purchase order if found</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PurchaseOrder>> GetPurchaseOrder(int id)
        {
            try
            {
                _logger.LogInformation("Getting purchase order with ID: {Id}", id);
                var purchaseOrder = await _purchaseOrderRepository.GetPurchaseOrderByIdAsync(id);
                
                if (purchaseOrder == null)
                {
                    _logger.LogWarning("Purchase order with ID: {Id} not found", id);
                    return NotFound($"Purchase order with ID {id} not found");
                }
                
                return Ok(purchaseOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting purchase order with ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        /// <summary>
        /// Gets purchase orders by vendor ID
        /// </summary>
        /// <param name="vendorId">The vendor ID</param>
        /// <returns>A collection of purchase orders for the specified vendor</returns>
        [HttpGet("vendor/{vendorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PurchaseOrder>>> GetPurchaseOrdersByVendorId(int vendorId)
        {
            try
            {
                _logger.LogInformation("Getting purchase orders for vendor ID: {VendorId}", vendorId);
                var purchaseOrders = await _purchaseOrderRepository.GetPurchaseOrdersByVendorIdAsync(vendorId);
                
                if (!purchaseOrders.Any())
                {
                    _logger.LogWarning("No purchase orders found for vendor ID: {VendorId}", vendorId);
                    return NotFound($"No purchase orders found for vendor with ID {vendorId}");
                }
                
                return Ok(purchaseOrders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting purchase orders for vendor ID: {VendorId}", vendorId);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        /// <summary>
        /// Creates a new purchase order
        /// </summary>
        /// <param name="purchaseOrder">The purchase order to create</param>
        /// <returns>The created purchase order</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PurchaseOrder>> CreatePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            try
            {
                if (purchaseOrder == null)
                {
                    return BadRequest("Purchase order data is null");
                }
                
                _logger.LogInformation("Creating new purchase order for vendor ID: {VendorId}", purchaseOrder.VendorId);
                var createdPurchaseOrder = await _purchaseOrderRepository.CreatePurchaseOrderAsync(purchaseOrder);
                
                return CreatedAtAction(nameof(GetPurchaseOrder), new { id = createdPurchaseOrder.PurchaseOrderId }, createdPurchaseOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating purchase order");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating purchase order");
            }
        }

        /// <summary>
        /// Updates an existing purchase order
        /// </summary>
        /// <param name="id">The purchase order ID</param>
        /// <param name="purchaseOrder">The updated purchase order data</param>
        /// <returns>No content if successful</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePurchaseOrder(int id, PurchaseOrder purchaseOrder)
        {
            try
            {
                if (purchaseOrder == null || id != purchaseOrder.PurchaseOrderId)
                {
                    return BadRequest("Invalid purchase order data or ID mismatch");
                }
                
                _logger.LogInformation("Updating purchase order with ID: {Id}", id);
                var existingPurchaseOrder = await _purchaseOrderRepository.GetPurchaseOrderByIdAsync(id);
                
                if (existingPurchaseOrder == null)
                {
                    _logger.LogWarning("Purchase order with ID: {Id} not found for update", id);
                    return NotFound($"Purchase order with ID {id} not found");
                }
                
                var result = await _purchaseOrderRepository.UpdatePurchaseOrderAsync(purchaseOrder);
                
                if (!result)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update purchase order");
                }
                
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating purchase order with ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating purchase order");
            }
        }

        /// <summary>
        /// Deletes a purchase order
        /// </summary>
        /// <param name="id">The purchase order ID to delete</param>
        /// <returns>No content if successful</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePurchaseOrder(int id)
        {
            try
            {
                _logger.LogInformation("Deleting purchase order with ID: {Id}", id);
                var existingPurchaseOrder = await _purchaseOrderRepository.GetPurchaseOrderByIdAsync(id);
                
                if (existingPurchaseOrder == null)
                {
                    _logger.LogWarning("Purchase order with ID: {Id} not found for deletion", id);
                    return NotFound($"Purchase order with ID {id} not found");
                }
                
                var result = await _purchaseOrderRepository.DeletePurchaseOrderAsync(id);
                
                if (!result)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete purchase order");
                }
                
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting purchase order with ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting purchase order");
            }
        }
    }
} 