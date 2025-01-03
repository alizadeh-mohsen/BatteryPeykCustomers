﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BatteryPeykCustomers.Model
{
    [Index(nameof(Phone))]
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string Phone { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string? Address { get; set; }
        public ICollection<Car>? Cars { get; set; }

        public bool IsCompany { get; set; }

    }
}
