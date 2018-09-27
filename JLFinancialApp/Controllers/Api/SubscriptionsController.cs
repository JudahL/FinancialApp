using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using JLFinancialApp.Models;
using JLFinancialApp.Models.DTOs;
using JLFinancialApp.Repositories;
using AutoMapper;
using System.Diagnostics;

namespace JLFinancialApp.Controllers.Api
{
    public class SubscriptionsController : ApiController
    {
        private readonly SubscriptionRepository _repository;
        private readonly PeriodTypeRepository _periodTypeRepository;
        private readonly ApplicationDbContext _context;

        public SubscriptionsController()
        {
            _context = ApplicationDbContext.Create();
            _repository = new SubscriptionRepository(_context);
            _periodTypeRepository = new PeriodTypeRepository(_context);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult PostSubscription(SubscriptionDTO subscriptionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }


            var subscription = Mapper.Map<SubscriptionDTO, Subscription>(subscriptionDTO);
            
            subscription.UserId = User.Identity.GetUserId();

            subscription.PeriodType = _periodTypeRepository.Get(subscriptionDTO.PeriodType.Id);

            _repository.Add(subscription);
            _context.SaveChanges();

            subscriptionDTO.Id = subscription.Id;

            return Created(new Uri(Request.RequestUri + "/" + subscription.Id), subscriptionDTO);
        }


        [HttpPut]
        [Authorize]
        public IHttpActionResult PutSubscription(int id, SubscriptionDTO subscriptionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var subscriptionInDb = _repository.Get(id);

            if (subscriptionInDb == null)
            {
                return NotFound();
            }

            Mapper.Map(subscriptionDTO, subscriptionInDb);

            var periodTypeInDb = _periodTypeRepository.Get(subscriptionDTO.PeriodType.Id);

            if (periodTypeInDb == null)
            {
                return NotFound();
            }

            Debug.WriteLine(periodTypeInDb.Name);
            Debug.WriteLine(periodTypeInDb.FrequencyPerYear);

            subscriptionInDb.PeriodType = periodTypeInDb;

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public IHttpActionResult DeleteSubscription(int id)
        {
            var subscriptionInDb = _repository.Get(id);

            if (subscriptionInDb == null)
            {
                return NotFound();
            }

            _repository.Remove(subscriptionInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
