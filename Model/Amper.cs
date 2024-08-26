using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Model
{
    public class Amper
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public int Amperage { get; set; }
    }
}
