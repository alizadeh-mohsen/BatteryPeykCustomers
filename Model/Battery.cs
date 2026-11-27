using BatteryPeykCustomers.Helpers.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BatteryPeykCustomers.Model
{
    public class Battery
    {
        [Key]
        public int Id { get; set; }

        public int AmperId { get; set; }

        public int BatteryBrandId { get; set; }

        public BatteryType BatteryType { get; set; }

        public string Title => $"{BatteryType} {Amper?.Title}";

        [ForeignKey("AmperId")]
        public required Amper Amper { get; set; }

        [ForeignKey("BrandId")]
        public required Brand Brand { get; set; }
    }
}
