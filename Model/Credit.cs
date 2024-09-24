using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace BatteryPeykCustomers.Model
{
    public class Credit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Amount { get; set; }

        public DateTime Date { get; set; } = DateTime.Today;

        [Required]
        public string? Description { get; set; }

    }
}
