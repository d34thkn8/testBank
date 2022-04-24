using System;

namespace Transaction.Api.model
{
    public class TransactionReportQuery:Validator
    {
        public int ClientId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public override (bool, string) IsValid()
        {

            if (ClientId <= 0)
                return (false, "ClientId field must be higher than zero");
            
            return base.IsValid();
        }

    }
}
