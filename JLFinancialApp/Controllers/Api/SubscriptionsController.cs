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
using AutoMapper;


namespace JLFinancialApp.Controllers.Api
{
    public class SubscriptionsController : ApiController
    {
        private ApplicationDbContext _context;

        public SubscriptionsController()
        {
            _context = ApplicationDbContext.Create();
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult PostSubscription(SubscriptionDTO subDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }


            var subscription = Mapper.Map<SubscriptionDTO, Subscription>(subDTO);
            
            subscription.UserId = User.Identity.GetUserId();

            subscription.PeriodType = _context.PeriodTypes.Single(pt => pt.Id == subscription.PeriodType.Id);

            _context.Subscriptions.Add(subscription);
            _context.SaveChanges();

            subDTO.Id = subscription.Id;

            return Created(new Uri(Request.RequestUri + "/" + subscription.Id), subDTO);
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

            var subscription = Mapper.Map<SubscriptionDTO, Subscription>(subscriptionDTO);
            var subscriptionInDb = _context.Subscriptions.SingleOrDefault(i => i.Id == id);            

            if (subscriptionInDb == null)
            {
                return NotFound();
            }

            var periodType = Mapper.Map<PeriodTypeDTO, PeriodType>(subscriptionDTO.PeriodType);
            var periodTypeInDb = _context.PeriodTypes.Single(pt => pt.Id == periodType.Id);

            if (periodTypeInDb == null)
            {
                return NotFound();
            }

            Mapper.Map(subscriptionDTO, subscriptionInDb);

            subscriptionInDb.PeriodType = periodTypeInDb;

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public IHttpActionResult DeleteSubscription(int id)
        {
            var subscriptionInDb = _context.Subscriptions.SingleOrDefault(s => s.Id == id);

            if (subscriptionInDb == null)
            {
                return NotFound();
            }

            _context.Subscriptions.Remove(subscriptionInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
