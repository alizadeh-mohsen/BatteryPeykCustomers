using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Helpers.Enums
{
    public enum StaffType
    {
        [Display(Name = "پاره وقت")]
        PartTime,
        [Display(Name = "تمام وقت")]
        FullTime
    }
}
