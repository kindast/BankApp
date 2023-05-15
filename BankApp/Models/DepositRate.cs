using System.ComponentModel.DataAnnotations.Schema;

namespace BankApp.Models
{
    public class DepositRate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Percent { get; set; }
        public bool IsCapitalization { get; set; }
        public bool IsReplenishment { get; set; }
        public bool IsWithdrawal { get; set; }

        [NotMapped]
        public string IsCapitalizationText { get => IsCapitalization ? "✓ Капитализация" : "✕ Без капитализации"; }

        [NotMapped]
        public string IsReplenishmentText { get => IsReplenishment ? "✓ Пополнение" : "✕ Без пополнения"; }

        [NotMapped]
        public string IsWithdrawalText { get => IsWithdrawal ? "✓ Снятие" : "✕ Без снятия"; }

        [NotMapped]
        public decimal Amount { get; set; }

        [NotMapped]
        public int Period { get; set; }

        [NotMapped]
        public decimal Total
        {
            get
            {
                decimal monthPercet = (decimal)(Percent / 12 / 100);

                decimal amount = Amount;
                if (IsCapitalization)
                    for (int i = 0; i < Period; i++)
                        amount += amount * monthPercet;
                else
                    for (int i = 0; i < Period; i++)
                        amount += Amount * monthPercet;

                return amount;
            }
        }

        [NotMapped]
        public decimal Income
        {
            get => Total - Amount;
        }
    }
}
