using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Pages.Admin.Customers
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Customer Customer { get; set; } = default!;

        private readonly Data.ApplicationDbContext _context;

        public DeleteModel(Data.ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToPage("./Index");

            if (id == null || _context.Customer == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FirstOrDefaultAsync(m => m.Id == id);

            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                Customer = customer;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!User.IsInRole("Admin"))
                return RedirectToPage("./Index");

            if (id == null)
            {
                return NotFound();
            }
            var customer = await _context.Customer.FindAsync(id);

            if (customer != null)
            {
                Customer = customer;
                _context.Customer.Remove(Customer);
                await _context.SaveChangesAsync();
                TempData["success"] = "Deleted Successfully";

            }

            return RedirectToPage("Index");
        }


    }
}
