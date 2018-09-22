using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using JLFinancialApp.Models;
using JLFinancialApp.Models.DTOs;
using AutoMapper;

namespace JLFinancialApp.Controllers.Api
{
    public class IncomeController : ApiController
    {
        private ApplicationDbContext _context;

        public IncomeController()
        {
            _context = ApplicationDbContext.Create();
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult PostIncome(IncomeDTO incomeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var income = Mapper.Map<IncomeDTO, Income>(incomeDTO);
            
            income.UserId = User.Identity.GetUserId();

            income.PeriodType = _context.PeriodTypes.Single(pt => pt.Id == income.PeriodType.Id);

            _context.Incomes.Add(income);
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

            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var income = Mapper.Map<IncomeDTO, Income>(incomeDTO);
            var incomeInDb = _context.Incomes.SingleOrDefault(i => i.Id == id);

            if(incomeInDb == null)
            {
                return NotFound();
            }

            var periodType = Mapper.Map<PeriodTypeDTO, PeriodType>(incomeDTO.PeriodType);
            var periodTypeInDb = _context.PeriodTypes.Single(pt => pt.Id == periodType.Id);

            if (periodTypeInDb == null)
            {
                return NotFound();
            }

            Mapper.Map(incomeDTO, incomeInDb);

            incomeInDb.PeriodType = periodTypeInDb;

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public IHttpActionResult DeleteIncome(int id)
        {
            var incomeInDb = _context.Incomes.SingleOrDefault(i => i.Id == id);

            if (incomeInDb == null)
            {
                return NotFound();
            }

            _context.Incomes.Remove(incomeInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
