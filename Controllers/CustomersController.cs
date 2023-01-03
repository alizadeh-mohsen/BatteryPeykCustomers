using Microsoft.AspNetCore.Mvc;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.EntityFrameworkCore;
using BatteryPeykCustomers.Helpers;

namespace BatteryPeykCustomers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{m}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(string m)
        {
            if (_context.Customer == null)
            {
                return NotFound();
            }
            var customer = await _context.Customer.FirstOrDefaultAsync(c => c.Phone == m);

            if (customer == null)
            {
                return NotFound();
            }

            var customerDto = new CustomerDto
            {
                Address = customer.Address,
                Battery = customer.Battery,
                Car = customer.Car,
                Name = customer.Name,
                Phone = customer.Phone,
                PurchaseDate = DateHelper.ToPersianDate(customer.PurchaseDate),
                Expire = setExpire(customer.PurchaseDate, customer.Guaranty),
                Status=setStatus(customer.PurchaseDate, customer.Guaranty)
            };

            //return Ok(customerDto);
            return Json(new { data = customerDto });
        }

        public string setExpire(DateTime purchaseDate, int guaranty)
        {
            var expire = purchaseDate.AddMonths(guaranty);

            return expire > DateTime.Today ? 
                "<span class='text-danger'>" + DateHelper.ToPersianDate(expire) + "</span>" :
                "<span class='text-success'>" + DateHelper.ToPersianDate(expire) + "</span>";
        }

        public string setStatus(DateTime purchaseDate, int lifeExpectancy)
        {
            var monthsPassed = DateHelper.CalcLife(purchaseDate);
            if (monthsPassed < lifeExpectancy / 2)
            {
                return "<span class='text-success'>سالم</span>";
            }
            else if ((lifeExpectancy / 2) < monthsPassed && monthsPassed < lifeExpectancy - 2)
            {
                return "<span class='text-warning'>سالم(احتیاط)</span>";
            }
            else
            {
                return "<span class= 'text-danger' > در اولین فرصت تعویض شود</span>";
            }
        }
    }
}


