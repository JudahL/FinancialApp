using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using JLFinancialApp.Models.DTOs;

namespace JLFinancialApp.Models.ViewModels
{
    public class RecurringAmountFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int? Amount { get; set; }

        public IEnumerable<PeriodTypeDTO> PeriodTypes { get; set; }

        [Required]
        public int? PeriodTypeId { get; set; }

        public string Title { get { return Id != 0 ? "Edit " + Type : "Add New " + Type; } }

        public string Type { get; set; }

        public string Controller { get; set; }

        public string Method { get; set; }

        public RecurringAmountFormViewModel()
        {
            Id = 0;
        }        
    }
}