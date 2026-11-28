using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BatteryPeykCustomers.Model;
using BatteryPeykCustomers.Data;

namespace BatteryPeykCustomers.Pages.ExpensePages;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Expense Expense { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var expense = await _context.Expense.FirstOrDefaultAsync(m => m.Id == id);
        if (expense is null)
        {
            return NotFound();
        }
        else
        {
            Expense = expense;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var expense = await _context.Expense.FindAsync(id);
        if (expense != null)
        {
            Expense = expense;
            _context.Expense.Remove(Expense);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
