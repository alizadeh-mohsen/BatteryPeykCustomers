using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;

namespace BatteryPeykCustomers.Pages.Admin.Guaranties
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Guarranty> Guarranty { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Guarranty = await _context.Guarranty
                .Include(g => g.Amper)
                .Include(g => g.Company).OrderBy(c => c.Id).ToListAsync();
        }
    }
}
