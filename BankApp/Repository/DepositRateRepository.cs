using BankApp.Data;
using BankApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace BankApp.Repository
{
    public class DepositRateRepository
    {
        private BankDbContext _context;

        public DepositRateRepository()
        {
            _context = BankDbContext.GetContext();
        }

        public ICollection<DepositRate> GetDepositsRates()
        {
            return _context.DepositsRates.ToList();
        }
    }
}
