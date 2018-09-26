using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using AutoMapper;
using JLFinancialApp.Models;
using JLFinancialApp.Models.ViewModels;
using JLFinancialApp.Models.DTOs;

namespace JLFinancialApp.Controllers
{
    public class IncomeController : Controller
    {
        private ApplicationDbContext _context;

        public IncomeController()
        {
            _context = ApplicationDbContext.Create();
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            IEnumerable<Income> incomes = _context.Incomes.Where(i => i.UserId == userId).Include(i => i.PeriodType).ToList();

            var vm = new RecurringAmountListViewModel(incomes, "Income", "Income", "primary");

            return View("RecurringAmountList", vm);
        }
        
        public ActionResult New()
        {
            var periodTypes = _context.PeriodTypes.ToList();

            var vm = new RecurringAmountFormViewModel()
            {
                PeriodTypes = Mapper.Map<List<PeriodType>, List<PeriodTypeDTO>>(periodTypes),
                Type = "income",
                Method = "post",
            };

            return View("RecurringAmountForm", vm);
        }

        public ActionResult Edit(int id)
        {
            var income = _context.Incomes.Include(s => s.PeriodType).SingleOrDefault(i => i.Id == id);

            if (income == null)
            {
                return HttpNotFound();
            }

            var vm = Mapper.Map<Income, RecurringAmountFormViewModel>(income);

            var periodTypes = _context.PeriodTypes.ToList();
            vm.PeriodTypes = Mapper.Map<List<PeriodType>, List<PeriodTypeDTO>>(periodTypes);
            vm.Type = "income";
            vm.Method = "put";

            return View("RecurringAmountForm", vm);

        }
    }
}