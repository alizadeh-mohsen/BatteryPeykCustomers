﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BatteryPeykCustomers.Model
{
    public class Debt
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;

        [Required]
        public uint Amount { get; set; }
        [Required]
        public string? Description { get; set; }
        //[Required]
        //public int? CounterpartyId { get; set; }
        //[Required]
        //public int? ReasonId { get; set; }
        //public DateTime? DueDate { get; set; }
        //public bool Settled { get; set; } = false;

        //[ForeignKey("ReasonId")]
        //public Reason? Reason { get; set; }

        //[ForeignKey("CounterpartyId")]
        //public Counterparty? Counterparty { get; set; }


    }
}
