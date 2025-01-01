using Microsoft.AspNetCore.Mvc;
using BatteryPeykCustomers.Data;
using BatteryPeykCustomers.Model;
using Microsoft.EntityFrameworkCore;

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
                    PurchaseDate = car.PurchaseDate.ToShortDateString(),
                    ReplacementDate = setReplacementDate(car.PurchaseDate, car.LifeExpectancy),
                    Status = setStatus(car.PurchaseDate, car.Guaranty),
                    BatteryAge = DateTime.Now.Date == car.PurchaseDate.Date ? "0"
                    : DateTime.Today.Subtract(car.PurchaseDate).TotalDays.ToString(),
                    IsCompany = customer.IsCompany,
                    Comment = car.Comments

                });
            }

            customerDto.CarDtos = carDtos;
            return Json(new { data = customerDto });
        }

        public string setExpire(DateTime purchaseDate, int guaranty)
        {
            var expire = purchaseDate.AddMonths(guaranty);
            var expireToDisplay = expire.ToShortDateString();
            return expire > DateTime.Today ?
                "<span class='text-success'>" + expireToDisplay + "</span>" :
            "<span class='text-danger'>" + expireToDisplay + "</span>";
        }

        public string setStatus(DateTime purchaseDate, int lifeExpectancy)
        {
            var replaceDate = purchaseDate.AddMonths(lifeExpectancy);
            var goodDate = replaceDate.AddDays(-30);
            if (DateTime.Today.Date <= goodDate.Date)
            {
                return "<span class='text-success'>سالم</span>";
            }
            else if (DateTime.Today.Date > goodDate && DateTime.Today <= replaceDate.Date)
            {
                return "<span class='text-warning'>سالم(احتیاط)</span>";
            }
            else
            {
                return "<span class= 'text-danger' > در اولین فرصت تعویض شود</span>";
            }
        }

        public string setReplacementDate(DateTime purchaseDate, int lifeExpectancy)
        {
            var replaceDate = purchaseDate.AddMonths(lifeExpectancy);
            var goodDate = replaceDate.AddDays(-30);
            var replaceDateDisplay = replaceDate.ToShortDateString();


            if (DateTime.Today.Date <= goodDate.Date)
            {
                return "<span class='text-success'>" +
                    replaceDateDisplay +
                    "</span>";
            }
            else if (DateTime.Today.Date > goodDate && DateTime.Today <= replaceDate.Date)
            {
                return "<span class='text-warning'>" +
                     replaceDateDisplay +
                    "</span>";
            }
            else
            {
                return "<span class= 'text-danger' >" +
                     replaceDateDisplay +
                    "</span>";
            }
        }
    }
}

//Added in MASTER BRANCH
//TEST2 CHANGED 1 
//TEST2 CHANGED 2 