using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Model.ViewModel
{
    public class CarAndCustomerViewModel
    {
        public  Customer Customer { get; set; }

        public IEnumerable<Car>? Cars { get; set; }
    }
}
