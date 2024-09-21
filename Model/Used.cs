using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Model
{
    public class Used
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }


        [Required]
        public int Amperage { get; set; }
    }
}
