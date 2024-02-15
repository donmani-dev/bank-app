namespace BankAppBackend.Models
{
    public class ConnectionStringsOptions
    {
        public const string ConnectionStrings = "ConnectionStrings";

        public string SQLConnectionString { get; set; } = string.Empty;
        public string RedisConnectionString { get; set; } = string.Empty;
    }
}
