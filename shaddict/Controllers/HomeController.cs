using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shaddict.Data;
using Shaddict.Models;

namespace Shaddict.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShadDictContext _context;

        public HomeController(ILogger<HomeController> logger, ShadDictContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Loading dashboard data...");
                
                var entityCount = await _context.Entities.CountAsync();
                var tableCount = await _context.DatabaseTables.CountAsync();
                var columnCount = await _context.TableColumns.CountAsync();
                
                _logger.LogInformation($"Dashboard data loaded: {entityCount} entities, {tableCount} tables, {columnCount} columns");
                
                var dashboardViewModel = new DashboardViewModel
                {
                    EntityCount = entityCount,
                    TableCount = tableCount,
                    ColumnCount = columnCount,
                    RecentTables = await _context.DatabaseTables
                        .OrderByDescending(t => t.Id)
                        .Take(5)
                        .Select(t => new DatabaseTable
                        {
                            Id = t.Id,
                            Name = t.Name,
                            Schema = t.Schema,
                            Description = t.Description,
                            EntityId = t.EntityId,
                            CreatedDate = DateTime.Now,
                            Entity = t.EntityId != null ? new Entity
                            {
                                Id = t.Entity.Id,
                                Name = t.Entity.Name,
                                Description = t.Entity.Description
                            } : null
                        })
                        .ToListAsync(),
                    EntitiesByTableCount = await _context.Entities
                        .Select(e => new EntityTableCount
                        {
                            EntityName = e.Name,
                            TableCount = e.Tables.Count
                        })
                        .OrderByDescending(e => e.TableCount)
                        .Take(5)
                        .ToListAsync()
                };

                return View(dashboardViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading dashboard data");
                return View(new DashboardViewModel());
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
