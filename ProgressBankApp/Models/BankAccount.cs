using ProgressBankApp.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text;

namespace ProgressBankApp.Models
{
    public class BankAccount
    {
        [Key]
        public string Number { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DateOpen { get; set; }
        [Required]
        public decimal Balance { get; set; }
        [Required]
        public BankAccountType Type { get; set; }
        public ICollection<History> Histories { get; set; }

        [NotMapped]
        public string FormattedBalance
        {
            get => Balance.ToString(Balance % 1 == 0 ? "N0" : "N2", CultureInfo.CreateSpecificCulture("ru-RU")) + "₽";
        }

        [NotMapped]
        public string TypeString
        {
            get => Type == BankAccountType.Checking ? "Расчетный" : "Вклад"; set { }
        }

        public static string GenerateUniqueNumber(int length)
        {
            AccountRepository accountRepository = new AccountRepository();
            string digits = "123456789";
            Random random = new Random();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < length; i++)
                stringBuilder.Append(digits[random.Next(0, digits.Length)]);

            if (!accountRepository.AccountExists(stringBuilder.ToString()))
                return stringBuilder.ToString();
            else
                return GenerateUniqueNumber(length);
        }
    }
}
