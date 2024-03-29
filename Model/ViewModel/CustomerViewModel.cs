﻿using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Model.ViewModel
{
    public class CustomerViewModel
    {
        [MaxLength(20)]
        [Required]
        public string Phone { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string? Address { get; set; }

        [MaxLength(50)]
        public string? Make { get; set; }

        [Required]
        [MaxLength(50)]
        public string Battery { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        public int Guaranty { get; set; }

        [Required]
        public int LifeExpectancy { get; set; }

        public string? Comments { get; set; }

        public DateTime ReplaceDate { get; set; }
    }
}
