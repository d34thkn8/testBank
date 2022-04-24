namespace Client.Api.model
{
    public class ClientRequestModel:Validator
    {
        public string IdCard { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public int Age { get; set; }
        public string Direction { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public override (bool, string) IsValid()
        {

            if (Age <= 0)
                return (false, "Age field must be higher than zero");

            if (string.IsNullOrEmpty(IdCard))
                return (false, "IdCard field is required");

            if (string.IsNullOrEmpty(Name))
                return (false, "Name is required");

            if (string.IsNullOrEmpty(Genre))
                return (false, "Genre is required");

            if (string.IsNullOrEmpty(Direction))
                return (false, "Direction is required");

            if (string.IsNullOrEmpty(Phone))
                return (false, "Phone is required");

            if (string.IsNullOrEmpty(Password))
                return (false, "Password is required");

            return base.IsValid();
        }
    }
}
