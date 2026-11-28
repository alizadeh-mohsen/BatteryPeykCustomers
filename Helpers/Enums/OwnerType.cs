using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Helpers.Enums
{
    public enum OwnerType
    {
        [Display(Name = "شخصی")]
        Individual,
        [Display(Name = "شرکتی")]
        Company
    }
}
