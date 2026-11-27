using BatteryPeykCustomers.Helpers.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Model
{
    [Index(nameof(Phone))]
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        public required string CustomerNumber { get; set; }

        [MaxLength(20)]
        [Required(ErrorMessage = "این فیلد اجباری است")]
        public required string Phone { get; set; }

        [Required(ErrorMessage = "این فیلد اجباری است")]
        [MaxLength(50)]
        public required string Name { get; set; }

        public string? Address { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public OwnerType CustomerType { get; set; }


    }
}
