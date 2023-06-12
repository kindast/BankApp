using ProgressBankApp.Models;
using System.Data.Entity;

namespace ProgressBankApp.Data
{
    public class BankDbContext : DbContext
    {
        private static BankDbContext _context;

        public static BankDbContext GetContext()
        {
            if (_context == null)
                _context = new BankDbContext();
            return _context;
        }

        public BankDbContext() : base("BankBaseConnection") { }

        public DbSet<User> Users { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<DepositRate> DepositsRates { get; set; }
    }
}
