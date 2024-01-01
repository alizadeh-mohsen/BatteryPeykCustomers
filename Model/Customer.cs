using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BatteryPeykCustomers.Model
{
    [Index(nameof(Phone))]
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string Phone { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string? Address { get; set; }
        public ICollection<Car>? Cars { get; set; }

    }
}
