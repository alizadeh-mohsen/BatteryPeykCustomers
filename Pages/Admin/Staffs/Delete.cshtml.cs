using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BatteryPeykCustomers.Model;
using BatteryPeykCustomers.Data;

namespace BatteryPeykCustomers.Pages.Admin.Staffs;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Employee Employee { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var employee = await _context.Employee.FirstOrDefaultAsync(m => m.Id == id);
        if (employee is null)
        {
            return NotFound();
        }
        else
        {
            Employee = employee;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var employee = await _context.Employee.FindAsync(id);
        if (employee != null)
        {
            Employee = employee;
            _context.Employee.Remove(Employee);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
