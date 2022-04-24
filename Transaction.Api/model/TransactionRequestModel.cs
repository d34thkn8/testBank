namespace Transaction.Api.model
{
    public class TransactionRequestModel:Validator
    {
        public int AccountId { get; set; }
        public decimal Value { get; set; }
        public override (bool, string) IsValid()
        {

            if (AccountId <= 0)
                return (false, "AccountId field must be higher than zero");
            if (Value == 0)
                return (false, "Value can't be 0");
            
            return base.IsValid();
        }
    }
}
