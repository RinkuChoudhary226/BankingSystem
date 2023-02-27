using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Models
{
    public class TransactionType
    {
        [Key]
        public int TransactionTypeId { get; set; }
        public string TransactionName { get; set; }
    }
}