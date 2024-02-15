namespace Models
{
    public enum UserType
    {
        TELLER,
        CUSTOMER
    }
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
    }
}
