using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Model
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "این فیلد اجباری است")]
        public int Amount { get; set; }
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public string? Description { get; set; }
        //[Required(ErrorMessage ="این فیلد اجباری است")]
        //public int CounterpartyId { get; set; }
        //[Required(ErrorMessage ="این فیلد اجباری است")]
        //public int ReasonId { get; set; }

        //[ForeignKey("ReasonId")]
        //public Reason? Reason { get; set; }

        //[ForeignKey("CounterpartyId")]
        //public Counterparty? Counterparty { get; set; }


    }
}
