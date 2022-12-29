using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Pages.Admin.Customers
{
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
            if (id == null || _context.Customer == null)
            {
                return NotFound();
            }

            var customer =  await _context.Customer.FirstOrDefaultAsync(m => m.Id == id);

            if (customer == null)
            {
                return NotFound();
            }
            Customer = customer;
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

                _context.Attach(Customer).State = EntityState.Modified;

                try
                {
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
