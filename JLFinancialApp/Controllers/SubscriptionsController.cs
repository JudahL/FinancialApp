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

namespace JLFinancialApp.Controllers
{
    public class SubscriptionsController : Controller
    {
        private ApplicationDbContext _context;

        public SubscriptionsController()
        {

            _context = ApplicationDbContext.Create();
        }
        // GET: Subscriptions
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            IEnumerable<Subscription> subscriptions = _context.Subscriptions.Where(s => s.UserId == userId).Include(s => s.PeriodType).ToList();

            var vm = new RecurringAmountListViewModel(subscriptions, "Subscription", "Subcriptions", "warning");

            return View("RecurringAmountList", vm);
        }
        
        // GET: Subscriptions/New
        public ActionResult New()
        {
            var periodTypes = _context.PeriodTypes.ToList();

            var vm = new RecurringAmountFormViewModel()
            {
                PeriodTypes = Mapper.Map<List<PeriodType>, List<PeriodTypeDTO>>(periodTypes),
                Type = "subscriptions",
                Method = "post",
            };

            return View("RecurringAmountForm",  vm);
        }

        public ActionResult Edit(int id)
        {
            var subscription = _context.Subscriptions.Include(s => s.PeriodType).SingleOrDefault(s => s.Id == id);

            if (subscription == null)
            {
                return HttpNotFound();
            }

            var vm = Mapper.Map<Subscription, RecurringAmountFormViewModel>(subscription);

            var periodTypes = _context.PeriodTypes.ToList();
            vm.PeriodTypes = Mapper.Map<List<PeriodType>, List<PeriodTypeDTO>>(periodTypes);
            vm.Type = "subscriptions";
            vm.Method = "put";

            return View("RecurringAmountForm", vm);

        }
    }
}