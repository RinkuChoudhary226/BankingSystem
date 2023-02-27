using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Models
{
    public class CustomerAccount
    {
        [Key]
        public int AccountId { get; set; }
        public string AccountNumber { get; set; }
        public int BankAccountTypeId { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal CurrentBalance { get; set; }
        public int CreatedById { get; set; }
        public int UpdatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime Updateddate { get; set; }
        public bool IsActive { get; set; }
    }
}
