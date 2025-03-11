using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TransitusWeb.Models;
using TransitusWeb.Services;

namespace TransitusWeb.Controllers;

/// <summary>
/// Controller for the home page and dashboard
/// </summary>
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApiService _apiService;

    /// <summary>
    /// Initializes a new instance of the HomeController class
    /// </summary>
    /// <param name="logger">The logger</param>
    /// <param name="apiService">The API service</param>
    public HomeController(ILogger<HomeController> logger, ApiService apiService)
    {
        _logger = logger;
        _apiService = apiService;
    }

    /// <summary>
    /// Displays the dashboard
    /// </summary>
    /// <returns>The dashboard view</returns>
    public async Task<IActionResult> Index()
    {
        try
        {
            // Get vendor count for the dashboard
            var vendors = await _apiService.GetAllVendorsAsync();
            ViewBag.VendorCount = vendors.Count();
            
            return View();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving dashboard data");
            ViewBag.VendorCount = 0;
            ViewBag.ErrorMessage = "Error retrieving dashboard data. Please try again later.";
            return View();
        }
    }

    /// <summary>
    /// Displays the privacy page
    /// </summary>
    /// <returns>The privacy view</returns>
    public IActionResult Privacy()
    {
        return View();
    }

    /// <summary>
    /// Displays the error page
    /// </summary>
    /// <returns>The error view</returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
} 