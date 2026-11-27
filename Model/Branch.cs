using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Model
{
    public class Branch
    {
        [Key]
        public int Id { get; set; }

        public required string Name { get; set; }
        public required string Code { get; set; }

        public ICollection<NewBattery> NewBatteries { get; set; } = new List<NewBattery>();
        public ICollection<OldBattery> OldBatteries { get; set; } = new List<OldBattery>();
        public ICollection<UsedBattery> UsedBatteries { get; set; } = new List<UsedBattery>();
        public ICollection<WarrantyBattery> WarrantyBatteries { get; set; } = new List<WarrantyBattery>();


    }
}
