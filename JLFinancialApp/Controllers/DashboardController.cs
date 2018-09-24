using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using JLFinancialApp.Models;
using JLFinancialApp.Models.ViewModels;
using JLFinancialApp.Models.DTOs;
using AutoMapper;

/*
 * TODO: Split logic from controller into a service
 */ 

namespace JLFinancialApp.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private ApplicationDbContext _context;

        public DashboardController()
        {
            _context = ApplicationDbContext.Create();
        }

        public ActionResult Index()
        {
            var incomes = _context.Incomes.Include(i => i.PeriodType).ToList();
            var incomeDTOs = Mapper.Map<IList<Income>, IList<IncomeDTO>>(incomes);

            var subscriptions = _context.Subscriptions.Include(s => s.PeriodType).ToList();
            var subscriptionDTOs = Mapper.Map<IList<Subscription>, IList<SubscriptionDTO>>(subscriptions);

            int totalIncome = CalculateTotalAmount(incomes);
            int totalCosts = CalculateTotalAmount(subscriptions);

            var vm = new DashboardViewModel(incomeDTOs.OrderByDescending(i => i.MonthlyAmount),
                                            subscriptionDTOs.OrderByDescending(s => s.MonthlyAmount),
                                            totalIncome,
                                            totalCosts);

            return View(vm);
        }

        private int CalculateTotalAmount<T>(IList<T> listOfAmounts) where T : RecurringAmount
        {
            var total = 0;

            foreach (RecurringAmount amount in listOfAmounts)
            {
                total += amount.MonthlyAmount;
            }

            return total;
        }
    }
}