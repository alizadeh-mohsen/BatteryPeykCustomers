using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Model
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        [Required]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } = null!;

        [Required]
        public int BatteryId { get; set; }
        public Battery Battery { get; set; } = null!;

        [Required]
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; } = null!;

        public DateTime PurchaseDate { get; set; }

        public DateTime ReplaceDate { get; set; }

        public string? Comments { get; set; }

        public int Sms { get; set; }

        public required string InvoiceNumber { get; set; }

    }
}
