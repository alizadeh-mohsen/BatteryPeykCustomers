
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Helpers;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCars.Pages.Admin.Cars
{
    [Authorize]
    public class EditModel : PageModel
    {
        [BindProperty]
        public Car Car { get; set; } = default!;

        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
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
            Car = res;
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

                _context.Attach(Car).State = EntityState.Modified;

                try
                {
                    Car.ReplaceDate = DateTime.Today.AddMonths(Car.LifeExpectancy);
                    await _context.SaveChangesAsync();
                    TempData["success"] = "Updated Successfully";

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!CarExists(Car.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                        return Page();
                    }
                }
                catch (DbUpdateException uex)
                {
                    if (uex.InnerException.Message != null && uex.InnerException.Message.ToLower().Contains("unique"))
                        ModelState.AddModelError("CarPhone", "Duplicate Phone");
                    return Page();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                    return Page();
                }

                var customer = await _context.Customer.FindAsync(Car.CustomerId);
                if (customer != null)
                {
                    SmsHelper smsHelper = new SmsHelper(customer.Name, customer.Phone);
                    var result = await smsHelper.SendSms(MessageType.Update);
                    if (result.Status != 1)
                    {
                        TempData["error"] = result.Message;
                    }
                }


                return RedirectToPage("Index", new { customerId = Car.CustomerId });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private bool CarExists(int id)
        {
            return (_context.Car?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
