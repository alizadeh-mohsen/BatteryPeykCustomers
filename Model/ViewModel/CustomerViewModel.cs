using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Model.ViewModel
{
    public class CustomerViewModel
    {
        [Required(ErrorMessage = "شماره موبایل را وارد کنید")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "نام را وارد کنید")]
        public string Name { get; set; }

        public string? Address { get; set; }

        public string? Make { get; set; }

        public string? Battery { get; set; }

        public DateTime? PurchaseDate { get; set; }

        [Required(ErrorMessage = "گارانتی را وارد کنید")]
        public int Guaranty { get; set; }

        [Required(ErrorMessage = "عمر مفید را وارد کنید")]
        public int LifeExpectancy { get; set; }

        public string? Comments { get; set; }

        public DateTime? ReplaceDate { get; set; }
        public int? VehicleId { get; set; }
        public int? CompanyId { get; set; }
        public int? AmperId { get; set; }
        public bool HasUsed { get; set; }
        public bool GuarrantyCustomer { get; set; }
        public int Profit { get; set; }
        public bool IsCompany { get; set; }
    }
}
