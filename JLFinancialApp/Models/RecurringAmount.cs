using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JLFinancialApp.Models
{
    public class RecurringAmount
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int Amount { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public PeriodType PeriodType { get; set; }
    }
}