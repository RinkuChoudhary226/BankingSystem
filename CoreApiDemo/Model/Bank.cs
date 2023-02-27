using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Models
{
    public class Bank
    {
        [Key]
        public int BankId { get; set; }
        public string BankName { get; set; } = string.Empty;
        public int CreatedById { get; set; }
        public int UpdatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime Updateddate { get; set; }

        public bool IsActive { get; set; }
    }
}


