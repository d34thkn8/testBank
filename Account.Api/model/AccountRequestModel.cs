namespace Account.Api.model
{
    public class AccountRequestModel : Validator
    {
        public string AccountNumber { get; set; }
        public int ClientId { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public override (bool, string) IsValid()
        {
            if (string.IsNullOrEmpty(AccountNumber))
                return (false, "AccountNumber field is required");
            if (string.IsNullOrEmpty(AccountType))
                return (false, "AccountType is required");

            if (ClientId<=0)
                return (false, "ClientId field must be higher than zero");

            return base.IsValid();
        }
    }
}
