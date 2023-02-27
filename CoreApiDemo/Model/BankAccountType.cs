using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Models
{
    public class BankAccountType
    {
        [Key]
        public int BankAccountTypeId { get; set; }
        public string AccountType { get; set; }
    }
}