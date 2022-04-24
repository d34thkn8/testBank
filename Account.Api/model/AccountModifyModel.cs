namespace Account.Api.model
{
    public class AccountModifyModel: AccountRequestModel
    {
        public int AccountId { get; set; }
        public bool Status { get; set; }
        public override (bool, string) IsValid()
        {

            if (AccountId <= 0)
                return (false, "AccountId field must be higher than zero");
            if (string.IsNullOrEmpty(AccountNumber))
                return (false, "AccountNumber field is required");
            if (string.IsNullOrEmpty(AccountType))
                return (false, "AccountType is required");

            if (ClientId <= 0)
                return (false, "ClientId field must be higher than zero");

            if (string.IsNullOrEmpty(AccountType))
                return (false, "AccountType is required");

            return base.IsValid();
        }
    }
}
