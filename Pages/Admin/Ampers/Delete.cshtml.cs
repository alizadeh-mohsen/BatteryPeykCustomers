using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Authorization;

using Microsoft.EntityFrameworkCore; namespace BatteryPeykCustomers.Pages.Admin.Ampers
{
        [Authorize] public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Amper Amper { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amper = await _context.Amper.FirstOrDefaultAsync(m => m.Id == id);

            if (amper == null)
            {
                return NotFound();
            }
            else
            {
                Amper = amper;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amper = await _context.Amper.FindAsync(id);
            if (amper != null)
            {
                Amper = amper;
                _context.Amper.Remove(Amper);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
