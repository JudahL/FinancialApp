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
    public class SubscriptionsController : ApiController
    {
        private ApplicationDbContext _context;

        public SubscriptionsController()
        {
            _context = ApplicationDbContext.Create();
        }

        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> PostSubscription(SubscriptionDTO subDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var user = User.Identity;

            var subscription = Mapper.Map<SubscriptionDTO, Subscription>(subDTO);
            subscription.User = _context.Users.Single(u => u.Id == user.GetUserId());

            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();

            subDTO.Id = subscription.Id;

            return Created(new Uri(Request.RequestUri + "/" + subscription.Id), subDTO);
        }
    }
}
