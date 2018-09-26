using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JLFinancialApp.Models;

namespace JLFinancialApp.Models.ViewModels
{
    public class RecurringAmountListViewModel
    {
        public IEnumerable<RecurringAmount> Amounts;

        public string RecurringType;

        public string ControllerName;

        public string Colour;

        public RecurringAmountListViewModel(IEnumerable<RecurringAmount> amounts, string recurringType, string controllerName, string colour)
        {
            Amounts = amounts;
            RecurringType = recurringType;
            ControllerName = controllerName;
            Colour = colour;
        }
    }
}