﻿using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly BatteryPeykCustomers.Data.ApplicationDbContext _context;

        public IndexModel(ILogger<IndexModel> logger,BatteryPeykCustomers.Data.ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;

        }

        public Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Customer == null)
            {
                return RedirectToPage("/NotFound");
            }

            var customer = await _context.Customer.FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return RedirectToPage("/NotFound");
            }
            else
            {
                Customer = customer;
            }
            return Page();
        }
    }
}
