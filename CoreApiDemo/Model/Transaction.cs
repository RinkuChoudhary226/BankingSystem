using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        [ForeignKey("CustomerAccount")]
        public int AccountId { get; set; }
        public DateTime TransactionDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
        [ForeignKey("TransactionType")]
        public int TransactionTypeId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime Updateddate { get; set; }
    }
}