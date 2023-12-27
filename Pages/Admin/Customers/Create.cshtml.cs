﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Authorization;
using BatteryPeykCustomers.Model.ViewModel;
using BatteryPeykCustomers.Data;

namespace BatteryPeykCustomers.Pages.Admin.Customers
{
    [Authorize]
    public class CreateModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        [BindProperty]
        public CustomerViewModel vm { get; set; } = default!;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {

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

                if (!vm.Phone.StartsWith("0"))
                {
                    ModelState.AddModelError("Customer.Phone", "Phone number should start with 0");
                    return Page();
                }

                var existingCustomer = _context.Customer.FirstOrDefault(x => x.Phone == vm.Phone);
                if (existingCustomer != null)
                {
                    ModelState.AddModelError("Customer.Phone", "Mobile phone exists");
                    return Page();
                }


                var Cars = new List<Car> { new()
                {
                    Battery = vm.Battery,
                    Guaranty = vm.Guaranty,
                    Make = vm.Make,
                    LifeExpectancy = vm.LifeExpectancy,
                    PurchaseDate = DateTime.Now,
                    ReplaceDate = DateTime.Today.AddMonths(vm.LifeExpectancy),
                    Comments = vm.Comments,
                } };
                Customer customer = new()
                {
                    Address = vm.Address,
                    Name = vm.Name,
                    Phone = vm.Phone,
                    Cars = Cars,
                };

                _context.Customer.Add(customer);
                await _context.SaveChangesAsync();
                var count = _context.Customer.Count();
                if (count % 100 == 0)
                {
                    TempData["success"] = " دمت گرم سلطان مشتریات شد" + count + " تا.";
                }
                else
                    TempData["success"] = "Created Successfully";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
