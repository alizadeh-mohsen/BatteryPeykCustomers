using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Pages.Admin.Customers
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly Data.ApplicationDbContext _context;

        public EditModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer =  await _context.Customer.FirstOrDefaultAsync(m => m.Id == id);

            if (customer == null)
            {
                return NotFound();
            }
            Customer = customer;


            var companies = await _context.Company.OrderBy(c => c.Title).ToListAsync();
            var vehicles = await _context.Vehicle.OrderBy(c => c.Make).ToListAsync();
            var ampers = await _context.Amper.OrderBy(c => c.Title).ToListAsync();
            ViewData["CompanyId"] = new SelectList(companies, "Id", "Title");
            ViewData["VehicleId"] = new SelectList(vehicles, "Id", "Make");
            ViewData["AmperId"] = new SelectList(ampers, "Id", "Title");

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

                if (!Customer.Phone.StartsWith("0"))
                {
                    ModelState.AddModelError("Customer.Phone", "Phone number should start with 0");
                    return Page();
                }

                var customerOld = _context.Customer.FirstOrDefault(c => c.Id != Customer.Id && c.Phone == Customer.Phone);
               if(customerOld != null)
                {
                    ModelState.AddModelError("Customer.Phone", "مشتری دیگری با این شماره تماس قبلا ثبت شده.");
                    return Page() ;

                }

                _context.Attach(Customer).State = EntityState.Modified;

                try
                {
                    //Customer.ReplaceDate = DateTime.Today.AddMonths(Customer.LifeExpectancy); 
                    await _context.SaveChangesAsync();
                    TempData["success"] = "Updated Successfully";

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!CustomerExists(Customer.Id))
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
                        ModelState.AddModelError("CustomerPhone", "Duplicate Phone");
                    return Page();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                    return Page();
                }

                return RedirectToPage("./Index");
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        private bool CustomerExists(int id)
        {
          return (_context.Customer?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
