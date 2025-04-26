namespace BatteryPeykCustomers.Model
{
    public class Expire
    {
        public int Id { get; set; }
        public int CustomerId {  get; set; }
        public string Phone { get; set; }

        public string Name { get; set; }

        public string? Address { get; set; }

        public string? Make { get; set; }

        public string Battery { get; set; }

        public DateTime PurchaseDate { get; set; }

        public int Guaranty { get; set; }

        public int LifeExpectancy { get; set; }

        public string? Comments { get; set; }

        public DateTime ReplaceDate { get; set; }
        public string? VipLink { get; set; }
    }
}
