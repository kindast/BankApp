using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgressBankApp.Models
{
    public class Gender
    {
        [Key]
        [MaxLength(1)]
        public string Code { get; set; }

        [NotMapped]
        public string Name { get => Code == "м" ? "мужской" : "женский"; }
    }
}
