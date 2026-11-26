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

        [ForeignKey("AmperId")]
        public Amper? Amper { get; set; }

        [ForeignKey("BatteryBrandId")]
        public BatteryBrand? BatteryBrand { get; set; }
        public BatteryType BatteryType { get; set; }
        public string Title
        {
            get { return $"{BatteryType} {Amper?.Title}"; }
        }

        public int Guaranty { get; set; }
        public int LifeExpectancy { get; set; }
    }
}
