using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BatteryPeykCustomers.Model
{
    public class Debt
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "این فیلد اجباری است")]
        public long Amount { get; set; }

        [Required(ErrorMessage = "این فیلد اجباری است")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "این فیلد اجباری است")]
        public int CounterpartyId { get; set; }

        [Required(ErrorMessage = "این فیلد اجباری است")]
        public int ReasonId { get; set; }

        public DateTime? DueDate { get; set; }

        public bool Settled { get; set; } = false;

        [ForeignKey(nameof(ReasonId))]
        public Reason Reason { get; set; } = null!;

        [ForeignKey(nameof(CounterpartyId))]
        public Counterparty Counterparty { get; set; } = null!;

    }
}
