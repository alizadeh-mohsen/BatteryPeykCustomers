using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Model
{
    public class NewBattery
    {
        [Key]
        public int Id { get; set; }

        public int BatteryId { get; set; }
        public required Battery Battery { get; set; }

        public int? BranchId { get; set; }
        public Branch? Branch { get; set; }

        [Required(ErrorMessage = "سود را وارد کنید")]
        public int Profit { get; set; }

        [Required(ErrorMessage = "این فیلد اجباری است")]
        public int Quantity { get; set; }

        public int AlertQuantity { get; set; }

    }
}
