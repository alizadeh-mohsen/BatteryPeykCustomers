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
                PurchaseDate = DateTime.Today

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

            if (!Customer.Phone.StartsWith("0"))
            {
                ModelState.AddModelError("Customer.Phone", "Phone number should start with 0");
                return Page();
            }

            var existingCustomer = _context.Customer.FirstOrDefault(x => x.Phone == Customer.Phone);
            if (existingCustomer != null)
            {
                ModelState.AddModelError("Customer.Phone", "Mobile phone exists");
                return Page();
            }
            
            _context.Customer.Add(Customer);
            await _context.SaveChangesAsync();
            TempData["success"] = "Created Successfully";
            return RedirectToPage("./Index");

        }
    }
}
