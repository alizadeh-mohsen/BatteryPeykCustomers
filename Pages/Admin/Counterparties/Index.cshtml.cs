using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BatteryPeykCustomers.Model;
using BatteryPeykCustomers.Data;

namespace BatteryPeykCustomers.Pages.Admin.Counterparties;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Counterparty> Counterparty { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Counterparty = await _context.Counterparty.ToListAsync();
    }
}
