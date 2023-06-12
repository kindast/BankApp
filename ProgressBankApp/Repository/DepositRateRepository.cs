using ProgressBankApp.Data;
using ProgressBankApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProgressBankApp.Repository
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

        public bool DepositRateUsed(int id)
        {
            return _context.Deposits.Any(d => d.DepositRate.Id == id);
        }

        public bool CreateDepositRate(DepositRate depositRate)
        {
            _context.DepositsRates.Add(depositRate);
            return Save();
        }

        public bool DeleteDepositRate(DepositRate depositRate)
        {
            _context.DepositsRates.Remove(depositRate);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
