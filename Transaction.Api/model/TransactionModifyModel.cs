using System;

namespace Transaction.Api.model
{
    public class TransactionModifyModel: TransactionRequestModel
    {
        public int TransactionId { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; }
        public decimal Balance { get; set; }
        public override (bool, string) IsValid()
        {

            if (TransactionId <= 0)
                return (false, "TransactionId field must be higher than zero");
            if (AccountId <= 0)
                return (false, "AccountId field must be higher than zero");
            if (Value == 0)
                return (false, "Value can't be 0");
            if (Balance < 0)
                return (false, "Balance can't be lower than 0");

            return base.IsValid();
        }
    }
}
