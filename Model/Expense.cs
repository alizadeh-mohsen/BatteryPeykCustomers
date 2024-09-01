using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BatteryPeykCustomers.Model
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        public int Amount { get; set; }
        public string? Description { get; set; }
        [Required]
        public int CounterpartyId { get; set; }
        [Required]
        public int ReasonId { get; set; }

        [ForeignKey("ReasonId")]
        public Reason? Reason { get; set; }

        [ForeignKey("CounterpartyId")]
        public Counterparty? Counterparty { get; set; }


    }
}
