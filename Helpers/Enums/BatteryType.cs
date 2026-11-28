using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Helpers.Enums
{
    public enum BatteryType
    {
        [Display(Name = "سبک")]
        Light,
        [Display(Name = "سنگین")]
        Heavy,
        [Display(Name = "خارجی")]
        International,
        [Display(Name = "موتورسیکلت")]
        Motorcycle,
        [Display(Name = "UPS")]
        UPS
    }
}
