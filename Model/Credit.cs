using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace BatteryPeykCustomers.Model
{
    public class Credit
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="این فیلد اجباری است")]
        public int Amount { get; set; }

        public DateTime Date { get; set; } = DateTime.Today;

        [Required(ErrorMessage ="این فیلد اجباری است")]
        public string? Description { get; set; }

    }
}
