using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Model
{
    public class Credit
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "این فیلد اجباری است")]
        public long Amount { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "این فیلد اجباری است")]
        public string? Description { get; set; }

    }
}
