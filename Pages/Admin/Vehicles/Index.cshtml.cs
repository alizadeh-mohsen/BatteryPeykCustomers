using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Helpers.Enums;
using BatteryPeykCustomers.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Pages.Admin.Vehicles
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? SearchNameString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchOwnerTypeString { get; set; }

        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Vehicle> Vehicle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            IQueryable<Vehicle> query = _context.Vehicle;

            if (!string.IsNullOrWhiteSpace(SearchNameString))
                query = query.Where(v => v.Name.Contains(SearchNameString));

            if (!string.IsNullOrWhiteSpace(SearchOwnerTypeString)
                && Enum.TryParse<OwnerType>(SearchOwnerTypeString, true, out var ownerType))
            {
                query = query.Where(v => v.OwnerType == ownerType);
            }

            Vehicle = await query.ToListAsync();
            return Page();
        }
    }
}
