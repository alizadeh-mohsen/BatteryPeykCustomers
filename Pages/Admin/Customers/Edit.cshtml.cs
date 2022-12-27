﻿using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Pages.Admin.Customers
{
    public class EditModel : PageModel
    {
        private readonly BatteryPeykCustomers.Data.ApplicationDbContext _context;

        public EditModel(BatteryPeykCustomers.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Customer == null)
            {
                return NotFound();
            }

            var customer =  await _context.Customer.FirstOrDefaultAsync(m => m.Phone == id);

            if (customer == null)
            {
                return NotFound();
            }
            Customer = customer;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
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
                if (!CustomerExists(Customer.Phone))
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

        private bool CustomerExists(string id)
        {
          return (_context.Customer?.Any(e => e.Phone == id)).GetValueOrDefault();
        }
    }
}
