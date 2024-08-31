using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BatteryPeykCustomers.Model
{
    public class Debt
    {
        [Key]
        public int Id { get; set; }
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public uint Amount { get; set; }
        public string? Description { get; set; }
        [Required]
        public int CounterpartyId { get; set; }
        [Required]
        public int ReasonId { get; set; }
        public DateOnly DueDate { get; set; }
        public bool Settled { get; set; } = false;

        [ForeignKey("ReasonId")]
        public Reason? Reason { get; set; }

        [ForeignKey("CounterpartyId")]
        public Counterparty? Counterparty { get; set; }


    }
}
