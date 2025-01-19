using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using Humanizer;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BatteryPeykCustomers.Pages.Admin.Guaranties
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public SelectList? CompanyList { get; set; }
        public SelectList? AmperList { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SelectedCompany { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SelectedAmper { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? From { get; set; }

        public IList<Guarranty> Guarranty { get; set; } = default!;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> OnGetAsync()
        {
            AmperList = new SelectList(await _context.Amper.OrderBy(c => c.Amperage).ToListAsync(), "Id", "Title");
            CompanyList = new SelectList(await _context.Company.OrderBy(c => c.Title).ToListAsync(), "Id", "Title");


            IQueryable<Guarranty> result = _context.Guarranty
                 .Include(g => g.Amper)
                .Include(g => g.Company)
                .OrderBy(c => c.Id);


            if (!string.IsNullOrEmpty(SelectedCompany))
                result = result.Where(t => t.CompanyId == int.Parse(SelectedCompany));

            if (!string.IsNullOrEmpty(SelectedAmper))
                result = result.Where(t => t.AmperId == int.Parse(SelectedAmper));

            if (From != null)
                result = result.Where(c => c.Date.Date == From);

            Guarranty = await result.ToListAsync();
            return Page();
        }
    }
}
