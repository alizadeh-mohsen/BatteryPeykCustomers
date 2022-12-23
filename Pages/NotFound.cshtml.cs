using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Pages
{
    public class NotFoundModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public UserPhone UserPhone { get; set; }

        public NotFoundModel(ApplicationDbContext context)
        {
            this._context = context;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!UserPhone.PhoneNumber.StartsWith("0"))
            { 
                ModelState.AddModelError("UserPhone.PhoneNumber", "Phone number should start with 0"); 
            }

            if (!ModelState.IsValid || _context.Customer == null)
            {
                return Page();
            }

            return RedirectToPage("/Index", new { m = UserPhone.PhoneNumber });
        }
    }
}
