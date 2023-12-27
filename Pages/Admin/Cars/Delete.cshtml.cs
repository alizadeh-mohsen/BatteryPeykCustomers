using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Pages.Admin.Cars
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Car Car { get; set; } = default!;
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var res = await _context.Car.FirstOrDefaultAsync(m => m.Id == id);

            if (res == null)
            {
                return NotFound();
            }
            else
            {
                Car = res;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var res = await _context.Car.FindAsync(id);

            if (res != null)
            {
                Car = res;
                _context.Car.Remove(Car);
                await _context.SaveChangesAsync();
                TempData["success"] = "Deleted Successfully";

            }

            return RedirectToPage("Index", new { customerId = Car.CustomerId });
        }
    }
}
