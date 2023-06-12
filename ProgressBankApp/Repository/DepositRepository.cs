using ProgressBankApp.Data;
using ProgressBankApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ProgressBankApp.Repository
{
    public class DepositRepository
    {
        private BankDbContext _context;

        public DepositRepository()
        {
            _context = BankDbContext.GetContext();
        }

        public ICollection<Deposit> GetDeposits()
        {
            return _context.Deposits.Include(d => d.DepositRate).Include(d => d.BankAccount).Include(d => d.BankAccount.Histories).OrderBy(a => a.BankAccount.DateOpen).ToList();
        }

        public ICollection<Deposit> GetDeposits(int clientId)
        {
            return _context.Deposits.Include(d => d.DepositRate).Include(d => d.BankAccount).Include(d => d.BankAccount.Histories).Where(a => a.BankAccount.User.Id == clientId).OrderBy(a => a.BankAccount.DateOpen).ToList();
        }

        public bool CreateDeposit(string bankAccountNumber, int depositRateId, int clientId, int months, decimal amount)
        {
            BankAccount bankAccountFrom = _context.BankAccounts.FirstOrDefault(a => a.Number == bankAccountNumber);
            if (bankAccountFrom == null || bankAccountFrom.Balance < amount) { return false; }

            bankAccountFrom.Balance -= amount;

            DepositRate depositRate = _context.DepositsRates.FirstOrDefault(r => r.Id == depositRateId);
            if (depositRate == null) { return false; }

            User user = _context.Users.FirstOrDefault(u => u.Id == clientId);
            if (user == null) { return false; }

            int count = _context.BankAccounts.Where(a => a.Name.Contains(depositRate.Name) && a.User.Id == user.Id).Count();

            BankAccount bankAccount = new BankAccount()
            {
                Number = BankAccount.GenerateUniqueNumber(10),
                Name = count > 0 ? $"{depositRate.Name} #{count + 1}" : depositRate.Name,
                User = user,
                DateOpen = DateTime.Now,
                Balance = amount,
                Type = _context.BankAccountTypes.FirstOrDefault(t => t.Name == "Вклад")
            };
            _context.BankAccounts.Add(bankAccount);

            History history = new History()
            {
                Account = bankAccount,
                Name = "Пополнение вклада",
                DateTime = DateTime.Now,
                Amount = amount
            };
            _context.Histories.Add(history);

            History historyFrom = new History()
            {
                Account = bankAccountFrom,
                Name = "Пополнение вклада",
                DateTime = DateTime.Now,
                Amount = -amount
            };
            _context.Histories.Add(historyFrom);

            Deposit deposit = new Deposit()
            {
                BankAccount = bankAccount,
                DepositRate = depositRate,
                DateExpiration = DateTime.Now.AddMonths(months),
                MonthsPassed = 0,
                Months = months
            };
            _context.Deposits.Add(deposit);

            return Save();
        }

        public bool DeleteDeposit(Deposit deposit)
        {
            string accountNumber = deposit.BankAccount.Number;

            _context.Deposits.Remove(deposit);
            _context.BankAccounts.Remove(_context.BankAccounts.FirstOrDefault(a => a.Number == accountNumber));
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
