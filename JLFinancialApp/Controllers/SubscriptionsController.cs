using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using AutoMapper;
using JLFinancialApp.Models.ViewModels;
using JLFinancialApp.Models.DTOs;
using JLFinancialApp.Models;
using JLFinancialApp.Repositories;

namespace JLFinancialApp.Controllers
{
    public class SubscriptionsController : Controller
    {
        private readonly SubscriptionRepository _repository;
        private readonly PeriodTypeRepository _periodTypeRepository;

        public SubscriptionsController()
        {
            var dbContext = ApplicationDbContext.Create();
            _repository = new SubscriptionRepository(dbContext);
            _periodTypeRepository = new PeriodTypeRepository(dbContext);
        }

        /// GET: Subscriptions
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            IEnumerable<Subscription> subscriptions = _repository.GetUserList(userId);

            var vm = new RecurringAmountListViewModel(subscriptions, "Subscription", "Subscriptions", "warning");

            return View("RecurringAmountList", vm);
        }
        
        /// GET: Subscriptions/New
        public ActionResult New()
        {
            var periodTypes = _periodTypeRepository.GetAll();

            var vm = new RecurringAmountFormViewModel()
            {
                PeriodTypes = Mapper.Map<IEnumerable<PeriodType>, IEnumerable<PeriodTypeDTO>>(periodTypes),
                Type = "Subscription",
                Controller = "subscriptions",
                Method = "post",
            };

            return View("RecurringAmountForm",  vm);
        }

        /// GET: Subscriptions/Edit/{id}
        public ActionResult Edit(int id)
        {
            var subscription = _repository.Get(id);

            if (subscription == null)
            {
                return HttpNotFound();
            }

            var vm = Mapper.Map<Subscription, RecurringAmountFormViewModel>(subscription);

            var periodTypes = _periodTypeRepository.GetAll();
            vm.PeriodTypes = Mapper.Map<IEnumerable<PeriodType>, IEnumerable<PeriodTypeDTO>>(periodTypes);
            vm.Type = "Subscription";
            vm.Controller = "subscriptions";
            vm.Method = "put";

            return View("RecurringAmountForm", vm);

        }
    }
}