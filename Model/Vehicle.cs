using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Model
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Make { get; set; }
    }
}
