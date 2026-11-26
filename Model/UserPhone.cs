using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Model
{
    public class UserPhone
    {
        [Required]
        public string PhoneNumber { get; set; }
    }
}
