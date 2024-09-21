using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;

namespace BatteryPeykCustomers.Pages.Admin.Useds
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Used Used { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var used = await _context.Used.FirstOrDefaultAsync(m => m.Id == id);

            if (used == null)
            {
                return NotFound();
            }
            else
            {
                Used = used;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var used = await _context.Used.FindAsync(id);
            if (used != null)
            {
                Used = used;
                _context.Used.Remove(Used);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
