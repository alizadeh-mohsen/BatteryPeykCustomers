using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;

namespace BatteryPeykCustomers.Pages.Admin.Credits
{
    public class IndexModel : PageModel
    {
        private readonly BatteryPeykCustomers.Data.ApplicationDbContext _context;

        public IndexModel(BatteryPeykCustomers.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Credit> Credit { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Credit = await _context.Credit.ToListAsync();
        }
    }
}
