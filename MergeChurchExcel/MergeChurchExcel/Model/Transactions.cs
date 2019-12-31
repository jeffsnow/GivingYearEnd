using System;

namespace MergeChurchExcel.Model
{
    public class Transactions
    {
        public int CheckNumber { get; set; }
        public DateTime Date { get; set; }
        public string Person { get; set; }
        public string Category { get; set; }
        public decimal CheckAmount { get; set; }
        public decimal CashAmount { get; set; }
        public decimal TotalAmount { get { return CheckAmount + CashAmount; } }
    }
}