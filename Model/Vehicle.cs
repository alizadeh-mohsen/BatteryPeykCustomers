using BatteryPeykCustomers.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Model
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public required string Name { get; set; }
        public OwnerType OwnerType { get; set; }

    }
}
