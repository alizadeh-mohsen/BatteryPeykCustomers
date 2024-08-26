using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;

namespace BatteryPeykCustomers.Pages.Admin.Brands
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Brand Brand { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Brand = await _context.Brand.FirstOrDefaultAsync(m => m.Id == id);

            if (Brand == null)
            {
                return NotFound();
            }
            else
            {
                Brand = Brand;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Brand = await _context.Brand.FindAsync(id);
            if (Brand != null)
            {
                Brand = Brand;
                _context.Brand.Remove(Brand);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
