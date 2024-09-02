using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;

namespace BatteryPeykCustomers.Pages.Admin.Batteries
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Battery> Battery { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Battery = await _context.Battery
                .Include(b => b.Amper)
                .Include(b => b.Company).OrderBy(c => c.Company.Title).ThenBy(c=>c.Amper.Amperage)
                .ToListAsync();
        }
    }
}
