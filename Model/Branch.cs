using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Model
{
    public class Branch
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int NewBatteryId { get; set; }
        public int OldBatteryId { get; set; }
        public int UsedBatteryId { get; set; }
        public int GuarrantyBatteryId { get; set; }


        public required NewBattery NewBattery { get; set; }
        public required OldBattery OldBattery { get; set; }
        public required UsedBattery UsedBattery { get; set; }
        public required GuarrantyBattery GuarrantyBattery { get; set; }


    }
}
