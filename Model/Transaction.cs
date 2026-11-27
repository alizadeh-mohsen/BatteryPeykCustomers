using BatteryPeykCustomers.Helpers.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BatteryPeykCustomers.Model
{
    public class Transaction
    {
        public int Id { get; set; }

        public int BankAccountId { get; set; }

        [ForeignKey("BankAccountId")]
        public required BankAccount BankAccount { get; set; }

        public required DateTime Date { get; set; } = DateTime.Now;
        public long Amount { get; set; }
        public TransactionType TransactionType { get; set; }

        public Reason? Reason { get; set; }

        public int? OrderId { get; set; }

    }
}
