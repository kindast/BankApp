using System;
using System.Collections.Generic;

namespace BankApp.Model
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public List<BankCard> BankCards { get; set; }
        public List<Transaction> Transactions { get; set; }

        public User() { }
        public User(string lastName, string firstName, string middleName, 
            string login, string password, List<BankCard> bankCards, List<Transaction> transactions)
        {
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            Login = login;
            Password = password;
            BankCards = bankCards;
            Transactions = transactions;
        }
    }

    public class BankCard
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public double Balance { get; set; }

        public BankCard() { }
        public BankCard(string name, string number, double balance)
        {
            Name = name;
            Number = number;
            Balance = balance;
        }
    }

    public class Transaction
    {
        public string Type { get; set; }
        public string Text { get; set; }
        public DateTime TransactionDateTime { get; set; }

        public Transaction() { }
        public Transaction(string type, string text, DateTime dateTime)
        {
            Type = type;
            Text = text;
            TransactionDateTime = dateTime;
        }
    }
}
