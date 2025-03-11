using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TransitusWeb.Models;

namespace TransitusWeb.Controllers
{
    public class VendorsController : Controller
    {
        // Temporary in-memory storage for vendors
        // Will be replaced with database access later
        private static List<Vendor> _vendors = new List<Vendor>
        {
            new Vendor
            {
                Id = 1,
                Name = "Acme Supplies",
                ContactName = "John Doe",
                Email = "john.doe@acme.com",
                Phone = "555-123-4567",
                Status = VendorStatus.Active,
                Category = "Office Supplies"
            },
            new Vendor
            {
                Id = 2,
                Name = "Tech Solutions Inc.",
                ContactName = "Jane Smith",
                Email = "jane.smith@techsolutions.com",
                Phone = "555-987-6543",
                Status = VendorStatus.Active,
                Category = "IT Services"
            },
            new Vendor
            {
                Id = 3,
                Name = "Global Logistics",
                ContactName = "Robert Johnson",
                Email = "robert@globallogistics.com",
                Phone = "555-456-7890",
                Status = VendorStatus.Inactive,
                Category = "Shipping"
            }
        };

        // GET: Vendors
        public IActionResult Index()
        {
            return View(_vendors);
        }

        // GET: Vendors/Details/5
        public IActionResult Details(int id)
        {
            var vendor = _vendors.FirstOrDefault(v => v.Id == id);
            if (vendor == null)
            {
                TempData["ErrorMessage"] = "Vendor not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(vendor);
        }

        // GET: Vendors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vendors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                // Assign a new ID (in a real app, the database would handle this)
                vendor.Id = _vendors.Count > 0 ? _vendors.Max(v => v.Id) + 1 : 1;
                
                _vendors.Add(vendor);
                TempData["SuccessMessage"] = "Vendor created successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(vendor);
        }

        // GET: Vendors/Edit/5
        public IActionResult Edit(int id)
        {
            var vendor = _vendors.FirstOrDefault(v => v.Id == id);
            if (vendor == null)
            {
                TempData["ErrorMessage"] = "Vendor not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(vendor);
        }

        // POST: Vendors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Vendor vendor)
        {
            if (id != vendor.Id)
            {
                TempData["ErrorMessage"] = "Invalid vendor ID.";
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                var existingVendor = _vendors.FirstOrDefault(v => v.Id == id);
                if (existingVendor == null)
                {
                    TempData["ErrorMessage"] = "Vendor not found.";
                    return RedirectToAction(nameof(Index));
                }

                // Update the existing vendor with new values
                existingVendor.Name = vendor.Name;
                existingVendor.ContactName = vendor.ContactName;
                existingVendor.Email = vendor.Email;
                existingVendor.Phone = vendor.Phone;
                existingVendor.Status = vendor.Status;
                existingVendor.Category = vendor.Category;

                TempData["SuccessMessage"] = "Vendor updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(vendor);
        }

        // GET: Vendors/Delete/5
        public IActionResult Delete(int id)
        {
            var vendor = _vendors.FirstOrDefault(v => v.Id == id);
            if (vendor == null)
            {
                TempData["ErrorMessage"] = "Vendor not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(vendor);
        }

        // POST: Vendors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var vendor = _vendors.FirstOrDefault(v => v.Id == id);
            if (vendor != null)
            {
                _vendors.Remove(vendor);
                TempData["SuccessMessage"] = "Vendor deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Vendor not found.";
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
} 