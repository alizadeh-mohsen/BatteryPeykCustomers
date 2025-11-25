using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace BatteryPeykCars.Pages.Admin.Cars
{
    [Authorize]
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Car Car { get; set; }
        [BindProperty]
        public int SelectedCarId { get; set; }
        [BindProperty]
        public int SelectedAmperId { get; set; }
        [BindProperty]
        public int SelectedBatteryId { get; set; }

        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet(int customerId)
        {
            Car = new Car { CustomerId = customerId };

            var companiesQuery = _context.Company.OrderBy(c => c.Title) as IQueryable<Company>;
            var vehiclesQuery = _context.Vehicle.OrderBy(c => c.Make) as IQueryable<Vehicle>;
            var ampersQuery = _context.Amper.OrderBy(c => c.Amperage) as IQueryable<Amper>;

            var companies = await companiesQuery.ToListAsync();
            var vehicles = await vehiclesQuery.ToListAsync();
            var ampers = await ampersQuery.ToListAsync();
            ViewData["Companies"] = new SelectList(companies, "Id", "Title");
            ViewData["Vehicles"] = new SelectList(vehicles, "Id", "Make");
            ViewData["Ampers"] = new SelectList(ampers, "Id", "Title");

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return Page();
                }

                var selectedBattery = await _context.Company.FindAsync(SelectedBatteryId);
                var selectedCar = await _context.Vehicle.FindAsync(SelectedCarId);
                var selectedAmper = await _context.Amper.FindAsync(SelectedAmperId);

                Car.Make = selectedCar.Make;
                Car.Battery = selectedBattery.Title + " " + selectedAmper.Title;

                Car.PurchaseDate = DateTime.Today;
                Car.ReplaceDate = DateTime.Today.AddMonths(Car.LifeExpectancy);

                _context.Car.Add(Car);
                await _context.SaveChangesAsync();

                TempData["success"] = "Created Successfully";


                return RedirectToPage("Index", new { customerId = Car.CustomerId });
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return Page();
            }
        }
        public async Task<JsonResult> OnGetCompanyAsync(int companyId)
        {
            var battery = await _context.Company.FirstOrDefaultAsync(c => c.Id == companyId);
            return new JsonResult(battery);
        }
    }
}
