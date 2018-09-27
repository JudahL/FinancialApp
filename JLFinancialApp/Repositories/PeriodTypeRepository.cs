using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using JLFinancialApp.Models;

namespace JLFinancialApp.Repositories
{
    public class PeriodTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public PeriodTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public PeriodType Get(int id)
        {         
            return _context
                .PeriodTypes
                .Single(pt => pt.Id == id);
        }

        public IEnumerable<PeriodType> GetAll()
        {
            return _context
                .PeriodTypes
                .ToList();
        }
    }
}