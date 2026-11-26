using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Model
{
    public class GuarrantyBattery
    {
        [Key]
        public int Id { get; set; }
        public int BatteryId { get; set; }
        public required Battery Battery { get; set; }

        [Required(ErrorMessage = "این فیلد اجباری است")]
        public int Quantity { get; set; }

    }
}
