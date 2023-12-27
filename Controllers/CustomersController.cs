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
            var customer = await _context.Customer.Include(c => c.Cars).FirstOrDefaultAsync(c => c.Phone == m);

            if (customer == null)
            {
                return NotFound();
            }

            var customerDto = new CustomerDto
            {
                Address = customer.Address,
                Name = customer.Name,
                Phone = customer.Phone
            };

            List<CarDto> carDtos = new List<CarDto>();
            foreach (var car in customer.Cars)
            {
                carDtos.Add(new CarDto
                {
                    Battery = car.Battery,
                    Make = car.Make,
                    Expire = setExpire(car.PurchaseDate, car.Guaranty),
                    PurchaseDate = car.PurchaseDate.ToPersianDate(),
                    Status = setStatus(car.PurchaseDate, car.Guaranty),
                    BatteryAge = DateTime.Today.Subtract(car.PurchaseDate).TotalDays.ToString()
                });
            }

            customerDto.CarDtos = carDtos;
            return Json(new { data = customerDto });
        }

        public string setExpire(DateTime purchaseDate, int guaranty)
        {
            var expire = purchaseDate.AddMonths(guaranty);
            return expire > DateTime.Today ?
                "<span class='text-success'>" + expire.ToPersianDate() + "</span>" :
            "<span class='text-danger'>" + expire.ToPersianDate() + "</span>";
        }

        public string setStatus(DateTime purchaseDate, int lifeExpectancy)
        {
            var monthsPassed = purchaseDate.CalcLife();
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


