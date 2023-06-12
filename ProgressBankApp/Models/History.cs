using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace ProgressBankApp.Models
{
    public class History
    {
        public int Id { get; set; }
        [Required]
        public BankAccount Account { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public decimal Amount { get; set; }

        [NotMapped]
        public string FormattedAmount
        {
            get
            {
                string formattedAmount = Amount.ToString(Amount % 1 == 0 ? "N0" : "N2", CultureInfo.CreateSpecificCulture("ru-RU")) + "₽";
                if (Amount > 0)
                    formattedAmount = "+" + formattedAmount;
                return formattedAmount;
            }
        }
    }
}
