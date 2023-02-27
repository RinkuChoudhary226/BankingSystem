using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Model
{
    public class Statement
    {
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime StartDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime EndDate { get; set; }
        public List<TransactionList> transList { get; set; } = new List<TransactionList>();
    }
    public class TransactionList
    {
        public string CustomerName { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
        public string Remarks { get; set; }
    }
}
