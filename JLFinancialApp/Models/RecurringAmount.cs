using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required]
        public PeriodType PeriodType { get; set; }

        public int MonthlyAmount
        {
            get
            {
                return (int)(Amount * (PeriodType.FrequencyPerYear / 12f));
            }
        }
    }
}