namespace BatteryPeykCustomers.Model.ViewModel
{
    public class LossAndProfitViewModel
    {
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public long Profit { get; set; }
        public int Amount { get; set; }
        public long TotalProfit { get; set; }
        public long TotalLoss { get; set; }
        public int TotalBatterySold { get; set; }
    }
}
