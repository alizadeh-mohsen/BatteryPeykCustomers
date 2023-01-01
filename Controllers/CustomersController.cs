using Microsoft.AspNetCore.Mvc;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.EntityFrameworkCore;

namespace BatteryPeykCustomers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{m}")]
        public async Task<ActionResult<Customer>> GetCustomer(string m)
        {
          if (_context.Customer == null)
          {
              return NotFound();
          }
            var customer = await _context.Customer.FirstOrDefaultAsync(c=>c.Phone==m);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);

        }


        private bool CustomerExists(int id)
        {
            return (_context.Customer?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
