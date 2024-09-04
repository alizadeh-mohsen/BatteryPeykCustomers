namespace BatteryPeykCustomers.Model.ViewModel
{
    public class CarAndCustomerViewModel
    {
        public  Customer Customer { get; set; }

        public IEnumerable<Car>? Cars { get; set; }
    }
}
