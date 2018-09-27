using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using JLFinancialApp.Models;

namespace JLFinancialApp.Repositories
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<PeriodType> PeriodTypes { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}