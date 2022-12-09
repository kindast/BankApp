using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BankApp.Model
{
    public class Bank
    {
        public Bank()
        {
            LoadUsers();
        }

        List<User> users;
        int currentUser;

        public List<User> Users { get { return users; } }
        public User CurrentUser { get { return users[currentUser]; } }

        private void LoadUsers()
        {
            if (File.Exists("Users.json"))
                users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText("Users.json"));
            else
                users = new List<User>();
        }

        private void SaveUsers()
        {
            while(CurrentUser.Transactions.Count > 10)
                CurrentUser.Transactions.RemoveAt(0);

            File.WriteAllText("Users.json", JsonConvert.SerializeObject(users));
        }

        public void Login(string login, string password)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Login == login && users[i].Password == password)
                {
                    currentUser = i;
                    return;
                }
            }
            throw new Exception("Неверный логин или пароль");
        }

        public void Register(string lastName, string firstName, string middleName, string login, string password)
        {
            User user = new User(lastName, firstName, middleName, login, password, new List<BankCard>(), new List<Transaction>());
            users.Add(user);
            SaveUsers();
        }

        public void CreateCard(string cardName)
        {
            if (CurrentUser.BankCards.Count >= 3)
                throw new Exception("В нашем банке можно иметь только 3 карты");

            foreach (var card in CurrentUser.BankCards)
            {
                if (card.Name == cardName)
                    throw new Exception("Карта с таким именем уже есть");
            }

            StringBuilder number = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < 16; i++)
                number.Append(random.Next(0, 10));

            BankCard bankCard = new BankCard(cardName, number.ToString(), 0);
            CurrentUser.BankCards.Add(bankCard);

            string transactionText = $"MyBank | Открытие счёта\n" +
                $"Дата совершения операции: {DateTime.Now}\n" +
                $"================================\n" +
                $"ФИО клиента: {CurrentUser.LastName} {CurrentUser.FirstName} {CurrentUser.MiddleName}\n" +
                $"Название новой карты: {CurrentUser.BankCards[CurrentUser.BankCards.Count - 1].Name}\n" +
                $"Номер новой карты: {CurrentUser.BankCards[CurrentUser.BankCards.Count - 1].Number}";
            CurrentUser.Transactions.Add(new Transaction("Открытие счёта", transactionText, DateTime.Now));
            SaveUsers();
        }

        public void DeleteCard(int indexCard)
        {
            string transactionText = $"MyBank | Закрытие счёта\n" +
                $"Дата совершения операции: {DateTime.Now}\n" +
                $"================================\n" +
                $"ФИО клиента: {CurrentUser.LastName} {CurrentUser.FirstName} {CurrentUser.MiddleName}\n" +
                $"Название карты: {CurrentUser.BankCards[indexCard].Name}\n" +
                $"Номер карты: {CurrentUser.BankCards[indexCard].Number}";
            CurrentUser.Transactions.Add(new Transaction("Закрытие счёта", transactionText, DateTime.Now));

            CurrentUser.BankCards.RemoveAt(indexCard);
            SaveUsers();
        }

        public void AddMoney(int indexCard, double money)
        {
            CurrentUser.BankCards[indexCard].Balance += Math.Round(money, 2);
            string transactionText = $"MyBank | Пополнение счёта\n" +
                $"Дата совершения операции: {DateTime.Now}\n" +
                $"================================\n" +
                $"ФИО клиента: {CurrentUser.LastName} {CurrentUser.FirstName} {CurrentUser.MiddleName}\n" +
                $"Название карты: {CurrentUser.BankCards[indexCard].Name}\n" +
                $"Номер карты: {CurrentUser.BankCards[indexCard].Number}\n" +
                $"================================\n" +
                $"Баланс до пополнения карты: {CurrentUser.BankCards[indexCard].Balance - Math.Round(money, 2)}₽\n" +
                $"Баланс после пополнения карты: {CurrentUser.BankCards[indexCard].Balance}₽";
            CurrentUser.Transactions.Add(new Transaction("Пополнение счёта", transactionText, DateTime.Now));
            SaveUsers();
        }

        public void WithdrawMoney(int indexCard, double money)
        {
            if (money <= CurrentUser.BankCards[indexCard].Balance)
                CurrentUser.BankCards[indexCard].Balance -= Math.Round(money, 2);
            else
                throw new Exception("Недостаточно средств");
            string transactionText = $"MyBank | Вывод средств с счёта\n" +
                $"Дата совершения операции: {DateTime.Now}\n" +
                $"================================\n" +
                $"ФИО клиента: {CurrentUser.LastName} {CurrentUser.FirstName} {CurrentUser.MiddleName}\n" +
                $"Название карты: {CurrentUser.BankCards[indexCard].Name}\n" +
                $"Номер карты: {CurrentUser.BankCards[indexCard].Number}\n" +
                $"================================\n" +
                $"Баланс до вывода средств: {CurrentUser.BankCards[indexCard].Balance + Math.Round(money, 2)}₽\n" +
                $"Баланс после вывода средств: {CurrentUser.BankCards[indexCard].Balance}₽";
            CurrentUser.Transactions.Add(new Transaction("Вывод средств с счёта", transactionText, DateTime.Now));
            SaveUsers();
        }

        public void Transfer(int indexCard, string cardNumber, double money)
        {
            if (CurrentUser.BankCards[indexCard].Number == cardNumber)
                throw new Exception("Вы пытаетесь перевести деньги на эту же карту");
            foreach(var user in Users) 
            {
                foreach (var card in user.BankCards)
                {
                    if (card.Number == cardNumber)
                    {
                        card.Balance += Math.Round(money, 2);
                        CurrentUser.BankCards[indexCard].Balance -= Math.Round(money, 2);
                        string transactionText = $"MyBank | Перевод средств\n" +
                            $"Дата совершения операции: {DateTime.Now}\n" +
                            $"================================\n" +
                            $"ФИО клиента: {CurrentUser.LastName} {CurrentUser.FirstName} {CurrentUser.MiddleName}\n" +
                            $"Название карты: {CurrentUser.BankCards[indexCard].Name}\n" +
                            $"Номер карты: {CurrentUser.BankCards[indexCard].Number}\n" +
                            $"================================\n" +
                            $"Переведено на карту: {cardNumber}\n" +
                            $"Баланс до перевода средств: {CurrentUser.BankCards[indexCard].Balance + Math.Round(money, 2)}₽\n" +
                            $"Баланс после перевода средств: {CurrentUser.BankCards[indexCard].Balance}₽";
                        CurrentUser.Transactions.Add(new Transaction("Перевод средств", transactionText, DateTime.Now));
                        SaveUsers();
                        return;
                    }
                }
            }
            throw new Exception("Карта с данным номером не найдена");
        }
    }
}
