using System;

namespace Transaction.Api.model
{
    public class TransactionReportModel
    {
        public DateTime Date { get; set; }
        public string Client { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public decimal FinalBalance { get; set; }
        public bool Status { get; set; }
        public decimal StartBalance => FinalBalance - TransactionValue;
        public decimal TransactionValue { get; set; }
        public string Transaction { get; set; }
    }
}
