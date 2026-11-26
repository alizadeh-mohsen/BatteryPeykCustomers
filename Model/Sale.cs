using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BatteryPeykCustomers.Model
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int VehicleId { get; set; }
        [Required]
        public int BatteryId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ReplaceDate { get; set; }
        public string? Comments { get; set; }
        public int Sms { get; set; }

        [ForeignKey("CustomerId")]
        public required Customer? Customer { get; set; }
        [ForeignKey("VehicleId")]
        public required Vehicle Vehicle { get; set; }
        [ForeignKey("BatteryId")]
        public required Battery Battery { get; set; }

    }
}
