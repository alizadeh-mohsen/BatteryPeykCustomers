using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BatteryPeykCustomers.Model;
using BatteryPeykCustomers.Data;

namespace BatteryPeykCustomers.Pages.Admin.Counterparties;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;
    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public Counterparty Counterparty { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var counterparty = await _context.Counterparty.FirstOrDefaultAsync(m => m.Id == id);
        if (counterparty is null)
        {
            return NotFound();
        }
        else
        {
            Counterparty = counterparty;
        }

        return Page();
    }
}
