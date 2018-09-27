using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using JLFinancialApp.Models;
using JLFinancialApp.Models.ViewModels;
using JLFinancialApp.Models.DTOs;
using JLFinancialApp.Repositories;
using AutoMapper;

/*
 * TODO: Split logic from controller into a service
 */ 

namespace JLFinancialApp.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IncomeRepository _incomeRepository;
        private readonly SubscriptionRepository _subscriptionRepository;

        public DashboardController()
        {
            var dbContext = ApplicationDbContext.Create();
            _incomeRepository = new IncomeRepository(dbContext);
            _subscriptionRepository = new SubscriptionRepository(dbContext);
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var incomes = _incomeRepository.GetUserList(userId);
            var incomeDTOs = Mapper.Map<IList<Income>, IList<IncomeDTO>>(incomes);

            var subscriptions = _subscriptionRepository.GetUserList(userId);
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