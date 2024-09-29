using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCars.Pages.Admin.Cars
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        public Car car { get; set; } = default!;
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var carRes = await _context.Car.FirstOrDefaultAsync(m => m.Id == id);
            if (carRes == null)
            {
                return NotFound();
            }
            else
            {
                car = carRes;
            }
            return Page();
        }
    }
}
