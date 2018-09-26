using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JLFinancialApp.Models.DTOs
{
    public class PeriodTypeDTO
    {
        public byte Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public int FrequencyPerYear { get; set; }
    }
}