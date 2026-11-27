using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BatteryPeykCustomers.Model;
using BatteryPeykCustomers.Data;

namespace BatteryPeykCustomers.Pages.Admin.Staffs;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Employee> Employee { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Employee = await _context.Employee.ToListAsync();
    }
}
