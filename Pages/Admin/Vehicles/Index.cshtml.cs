using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;

namespace BatteryPeykCustomers.Pages.Admin.Vehicles
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? SearchNameString { get; set; }

        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Vehicle> Vehicle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            IQueryable<Vehicle> query = _context.Vehicle;

            if (!string.IsNullOrWhiteSpace(SearchNameString))
                query = query.Where(v => v.Make.Contains(SearchNameString));

            Vehicle = await query.ToListAsync();
            return Page();
        }
    }
}
