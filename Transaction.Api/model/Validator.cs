namespace Transaction.Api.model
{
    public abstract class Validator
    {
        public virtual (bool, string) IsValid()
        {
            return (true, "NO ERROR");
        }
    }
}
