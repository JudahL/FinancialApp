using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using JLFinancialApp.Models;

namespace JLFinancialApp.Repositories
{
    public class IncomeRepository
    {
        private readonly ApplicationDbContext _context;

        public IncomeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Income Get(int id)
        {
            return _context
                .Incomes
                .Include(i => i.PeriodType)
                .SingleOrDefault(i => i.Id == id);
        }

        public IList<Income> GetUserList(string userId)
        {
            return _context
                .Incomes
                .Where(i => i.UserId == userId)
                .Include(i => i.PeriodType)
                .ToList();
        }

        public void Add(Income income)
        {
            _context
                .Incomes
                .Add(income);
        }

        public void Remove(Income income)
        {
            _context
                .Incomes
                .Remove(income);
        }
    }
}