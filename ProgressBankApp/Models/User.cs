using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgressBankApp.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public Role Role { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public Gender Gender { get; set; }
        public string PassportSeries { get; set; }
        public string PassportNumber { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }

        [NotMapped]
        public string FullName { get => $"{Surname} {Name} {Patronymic}"; }

        [NotMapped]
        public string ShortName { get => $"{Surname} {Name.Substring(0, 1)}.{Patronymic.Substring(0, 1)}."; set => Name = Name; }

        [NotMapped]
        public string Passport
        {
            get => $"{PassportSeries}{PassportNumber}";
            set
            {
                PassportSeries = value.ToString().Substring(0, 4);
                PassportNumber = value.ToString().Substring(4, 6);
            }
        }
    }
}
