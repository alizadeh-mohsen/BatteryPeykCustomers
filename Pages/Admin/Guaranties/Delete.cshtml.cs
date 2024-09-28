using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BatteryPeykCustomers.Pages.Admin.Guaranties
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(BatteryPeykCustomers.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Guarranty Guarranty { get; set; } = default!;

        [BindProperty]
        public string DeleteType { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guarranty = await _context.Guarranty
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guarranty == null)
            {
                return NotFound();
            }
            Guarranty = guarranty;
            ViewData["AmperId"] = new SelectList(_context.Amper.OrderBy(c => c.Amperage), "Id", "Title");
            ViewData["CompanyId"] = new SelectList(_context.Company.OrderBy(c => c.Title), "Id", "Title");
            return Page();
        }




        public async Task<IActionResult> OnPostAsync(int? id)
        {

            var battery = await _context.Battery
                .Include(c => c.Company)
                .Include(c => c.Amper)
                .FirstOrDefaultAsync(c => c.AmperId == Guarranty.AmperId && c.CompanyId == Guarranty.CompanyId);
            if (battery == null)
            {
                var message = ("باتری انتخاب شده در انبار موجود نیست. ابتدا باتری مورد نظر را به انبار اضافه کنید");
                TempData["error"] = message;

                return await OnGetAsync(id);
            }

            var desc = battery.Company.Title + " " + battery.Amper.Title;
            if (DeleteType == "cash")
            {

                var credit = new Credit
                {
                    Amount = Guarranty.Amount,
                    Date = DateTime.Today,
                    Description = "تسویه نقدی گارانتی " + desc
                };
                _context.Credit.Add(credit);
                var message = $" به اعتبار خریداضافه شد {Guarranty.Amount}";
                TempData["success"] = message;
            }
            else if (DeleteType == "battery")
            {
                battery.Quantity += 1;

                var message = $"به موجودی {desc}  در انبار یک عدد اضافه شد";
                TempData["success"] = message;

            }

            var guaranty = await _context.Guarranty.FindAsync(id);
            if (guaranty != null)
            {
                Guarranty = guaranty;
                _context.Guarranty.Remove(Guarranty);
            }

            await _context.SaveChangesAsync();
            
            if (DeleteType == "battery")
                return RedirectToPage("./Index");
            else
                return RedirectToPage("/Admin/Credits/Index");

        }
    }
}
