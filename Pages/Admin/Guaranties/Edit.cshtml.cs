using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;

namespace BatteryPeykCustomers.Pages.Admin.Guaranties
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(BatteryPeykCustomers.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Guarranty Guarranty { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guarranty =  await _context.Guarranty.FirstOrDefaultAsync(m => m.Id == id);
            if (guarranty == null)
            {
                return NotFound();
            }
            Guarranty = guarranty;
           ViewData["AmperId"] = new SelectList(_context.Amper, "Id", "Title");
           ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Title");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Guarranty).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuarrantyExists(Guarranty.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool GuarrantyExists(int id)
        {
            return _context.Guarranty.Any(e => e.Id == id);
        }
    }
}
