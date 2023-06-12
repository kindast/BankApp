using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace ProgressBankApp.Models
{
    public class Deposit
    {
        public int Id { get; set; }
        [Required]
        public BankAccount BankAccount { get; set; }
        [Required]
        public DepositRate DepositRate { get; set; }
        public DateTime DateExpiration { get; set; }
        public int Months { get; set; }
        public int MonthsPassed { get; set; }
        public decimal Accumulated { get; set; }

        [NotMapped]
        public string FormattedAccumulated
        {
            get => Accumulated.ToString(Accumulated % 1 == 0 ? "N0" : "N2", CultureInfo.CreateSpecificCulture("ru-RU")) + "₽";
        }
    }
}
