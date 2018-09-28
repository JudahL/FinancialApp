using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using JLFinancialApp.Models;
using JLFinancialApp.Models.DTOs;
using JLFinancialApp.Repositories;
using AutoMapper;

namespace JLFinancialApp.Controllers.Api
{
    public class IncomeController : ApiController
    {
        private readonly IncomeRepository _repository;
        private readonly PeriodTypeRepository _periodTypeRepository;
        private readonly ApplicationDbContext _context;

        public IncomeController()
        {
            _context = ApplicationDbContext.Create();
            _repository = new IncomeRepository(_context);
            _periodTypeRepository = new PeriodTypeRepository(_context);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult PostIncome(IncomeDTO incomeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var income = Mapper.Map<IncomeDTO, Income>(incomeDTO);
            
            income.UserId = User.Identity.GetUserId();

            income.PeriodType = _periodTypeRepository.Get(incomeDTO.PeriodType.Id);

            _repository.Add(income);
            _context.SaveChanges();

            incomeDTO.Id = income.Id;

            return Created(new Uri(Request.RequestUri + "/" + income.Id), incomeDTO);
        }

        [HttpPut]
        [Authorize]
        public IHttpActionResult PutIncome(int id, IncomeDTO incomeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var incomeInDb = _repository.Get(id);

            if (incomeInDb == null)
            {
                return NotFound();
            }

            Mapper.Map(incomeDTO, incomeInDb);

            var periodTypeInDb = _periodTypeRepository.Get(incomeDTO.PeriodType.Id);

            if (periodTypeInDb == null)
            {
                return NotFound();
            }

            incomeInDb.PeriodType = periodTypeInDb;

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public IHttpActionResult DeleteIncome(int id)
        {
            var incomeInDb = _repository.Get(id);

            if (incomeInDb == null)
            {
                return NotFound();
            }

            _repository.Remove(incomeInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
