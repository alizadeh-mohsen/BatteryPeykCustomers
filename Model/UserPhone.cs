using Microsoft.Build.Framework;

namespace BatteryPeykCustomers.Model
{
    public class UserPhone
    {
        [Required]
        public string PhoneNumber { get; set; }
    }
}
