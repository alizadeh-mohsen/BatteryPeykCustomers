using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Model.ViewModel
{
    public class ExpireViewModel
    {
        public int Id { get; set; }
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
    }
}
