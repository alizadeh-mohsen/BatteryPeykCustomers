using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Pages.Admin.Customers
{
    public class CreateModel : PageModel
    {
        private readonly BatteryPeykCustomers.Data.ApplicationDbContext _context;

        public CreateModel(BatteryPeykCustomers.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Customer = new Customer
            {
                PurchaseDate = DateTime.Today,
                GuarantyStartDate = DateTime.Today,
            };
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Customer == null || Customer == null)
            {
                return Page();
            }
            try
            {
                _context.Customer.Add(Customer);
                await _context.SaveChangesAsync();
                TempData["success"] = "Created Successfully";
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException uex)
            {
                if (uex.InnerException.Message != null && uex.InnerException.Message.ToLower().Contains("unique"))
                    ModelState.AddModelError("CustomerPhone", "Duplicate Phone");
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                return Page();
            }
        }
    }
}
