using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Model
{
    public class Reason
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
