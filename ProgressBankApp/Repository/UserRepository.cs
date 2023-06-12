using ProgressBankApp.Data;
using ProgressBankApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace ProgressBankApp.Repository
{
    public class UserRepository
    {
        private BankDbContext _context;

        public UserRepository()
        {
            _context = BankDbContext.GetContext();
        }

        public bool UserExists(string login)
        {
            return _context.Users.Any(u => u.Login == login);
        }
        
        public bool UserExists(string login, string password)
        {
            return _context.Users.Any(u => u.Login == login && u.Password == password);
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.Include(u => u.Gender).Include(u => u.Role).ToList();
        }

        public ICollection<User> GetClients()
        {
            return _context.Users.Include(u => u.Gender).Include(u => u.Role).Where(u => u.Role.Name == "Клиент").ToList();
        }

        public User GetUser(int id)
        {
            return _context.Users.Include(u => u.Gender).Include(u => u.Role).FirstOrDefault(u => u.Id == id);
        }

        public User GetUser(string login)
        {
            return _context.Users.Include(u => u.Gender).Include(u => u.Role).FirstOrDefault(u => u.Login == login);
        }

        public Role GetClientRole()
        {
            return _context.Roles.FirstOrDefault(r => r.Name == "Клиент");
        }

        public Role GetManagerRole()
        {
            return _context.Roles.FirstOrDefault(r => r.Name == "Менеджер");
        }

        public ICollection<Gender> GetGenders()
        {
            return _context.Genders.ToList();
        }

        public bool CreateUser(User user)
        {
            _context.Users.Add(user);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
