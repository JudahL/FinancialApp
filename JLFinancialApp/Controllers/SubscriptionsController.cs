using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JLFinancialApp.Models.ViewModels;
using JLFinancialApp.Models.DTOs;
using JLFinancialApp.Models;
using AutoMapper;

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
            return View();
        }
        
        // GET: Subscriptions/New
        public ActionResult New()
        {
            var periodTypes = _context.PeriodTypes.ToList();

            var vm = new SubscriptionFormViewModel()
            {
                PeriodTypes = Mapper.Map<List<PeriodType>, List<PeriodTypeDTO>>(periodTypes)
            };

            return View("SubscriptionForm",  vm);
        }
    }
}