using ProgressBankApp.Data;
using ProgressBankApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ProgressBankApp.Repository
{
    public class BankAccountRepository
    {
        private BankDbContext _context;

        public BankAccountRepository()
        {
            _context = BankDbContext.GetContext();
        }

        public BankAccount GetAccount(string number)
        {
            return _context.BankAccounts.Include(a => a.Histories).Where(a => a.Number == number).FirstOrDefault();
        }

        public ICollection<BankAccount> GetAccounts(int clientId)
        {
            return _context.BankAccounts.Include(a => a.Histories).Where(a => a.User.Id == clientId && a.Type.Name == "Расчетный").OrderBy(a => a.DateOpen).ToList();
        }

        public bool AccountExists(string number)
        {
            return _context.BankAccounts.Any(a => a.Number == number);
        }

        public BankAccountType GetCheckingType()
        {
            return _context.BankAccountTypes.FirstOrDefault(t => t.Name == "Расчетный");
        }
        
        public BankAccountType GetDepositType()
        {
            return _context.BankAccountTypes.FirstOrDefault(t => t.Name == "Вклад");
        }

        public bool Transfer(BankAccount accountFrom, BankAccount accountTo, decimal amount)
        {
            if (accountFrom.Balance < amount)
                return false;

            accountTo.Balance += amount;
            accountTo.Histories.Add(
                new History()
                {
                    Name = "Перевод между счетами",
                    Account = accountTo,
                    Amount = amount,
                    DateTime = DateTime.Now,
                });

            accountFrom.Balance -= amount;
            accountFrom.Histories.Add(
                new History()
                {
                    Name = "Перевод между счетами",
                    Account = accountFrom,
                    Amount = -amount,
                    DateTime = DateTime.Now,
                });
            return Save();
        }

        public bool CreateAccount(BankAccount bankAccount)
        {
            _context.BankAccounts.Add(bankAccount);
            return Save();
        }

        public bool DeleteAccount(BankAccount bankAccount)
        {
            _context.BankAccounts.Remove(bankAccount);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
