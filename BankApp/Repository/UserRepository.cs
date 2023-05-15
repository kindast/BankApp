﻿using BankApp.Data;
using BankApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace BankApp.Repository
{
    public class UserRepository
    {
        private BankDbContext _context;

        public UserRepository()
        {
            _context = BankDbContext.GetContext();
        }

        public bool UserExists(string login, string password)
        {
            return _context.Users.Any(u => u.Login == login && u.Password == password);
        }

        public ICollection<User> GetClients()
        {
            return _context.Users.Where(u => u.Role == Role.Client).ToList();
        }

        public User GetClient(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public User GetClient(string login)
        {
            return _context.Users.FirstOrDefault(u => u.Login == login);
        }
    }
}
