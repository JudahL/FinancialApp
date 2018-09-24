using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JLFinancialApp.Models.DTOs
{
    public class SubscriptionDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int Amount { get; set; }
        
        [Required]
        public PeriodTypeDTO PeriodType { get; set; }

        public int MonthlyAmount { get; set; }
    }
}