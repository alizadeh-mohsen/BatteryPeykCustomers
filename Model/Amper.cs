using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Model
{
    public class Amper
    {
        [Key]
        public byte Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public byte Amperage { get; set; }
    }
}
