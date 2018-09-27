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
using JLFinancialApp.Repositories;

namespace JLFinancialApp.Controllers
{
    public class IncomeController : Controller
    {
        private readonly IncomeRepository _repository;
        private readonly PeriodTypeRepository _periodTypeRepository;

        public IncomeController()
        {
            var dbContext = ApplicationDbContext.Create();
            _repository = new IncomeRepository(dbContext);
            _periodTypeRepository = new PeriodTypeRepository(dbContext);
        }

        /// GET: Income
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            IEnumerable<Income> incomes = _repository.GetUserList(userId);

            var vm = new RecurringAmountListViewModel(incomes, "Income", "Income", "primary");

            return View("RecurringAmountList", vm);
        }

        /// GET: Income/New
        public ActionResult New()
        {
            var periodTypes = _periodTypeRepository.GetAll();

            var vm = new RecurringAmountFormViewModel()
            {
                PeriodTypes = Mapper.Map<IEnumerable<PeriodType>, IEnumerable<PeriodTypeDTO>>(periodTypes),
                Type = "income",
                Method = "post",
            };

            return View("RecurringAmountForm", vm);
        }

        /// GET: Income/Edit/{id}
        public ActionResult Edit(int id)
        {
            var income = _repository.Get(id);

            if (income == null)
            {
                return HttpNotFound();
            }

            var vm = Mapper.Map<Income, RecurringAmountFormViewModel>(income);

            var periodTypes = _periodTypeRepository.GetAll();
            vm.PeriodTypes = Mapper.Map<IEnumerable<PeriodType>, IEnumerable<PeriodTypeDTO>>(periodTypes);
            vm.Type = "income";
            vm.Method = "put";

            return View("RecurringAmountForm", vm);

        }
    }
}