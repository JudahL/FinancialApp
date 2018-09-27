using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using JLFinancialApp.Models;

namespace JLFinancialApp.Repositories
{
    public class SubscriptionRepository
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Subscription Get(int id)
        {
            return _context
                .Subscriptions
                .Include(i => i.PeriodType)
                .SingleOrDefault(i => i.Id == id);
        }

        public IList<Subscription> GetUserList(string userId)
        {
            return _context
                .Subscriptions
                .Where(i => i.UserId == userId)
                .Include(i => i.PeriodType)
                .ToList();
        }

        public void Add(Subscription subscription)
        {
            _context
                .Subscriptions
                .Add(subscription);
        }

        public void Remove(Subscription subscription)
        {
            _context
                .Subscriptions
                .Remove(subscription);
        }
    }
}