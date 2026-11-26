namespace BatteryPeykCustomers.Model.ViewModel
{
    public class CarAndCustomerViewModel
    {
        public  Customer Customer { get; set; }

        public IEnumerable<Sale>? Cars { get; set; }
    }
}
