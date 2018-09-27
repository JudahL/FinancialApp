using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JLFinancialApp.Models
{
    public class PeriodType
    {
        public byte Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public int FrequencyPerYear { get; set; }

        // PeriodType Ids
        public static readonly byte UNDEFINED = 0;
        public static readonly byte YEARLY = 1;
        public static readonly byte MONTHLY = 2;
        public static readonly byte WEEKLY = 3;
        public static readonly byte DAILY = 4;
    }
}