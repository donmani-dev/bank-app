using System.Text.Json.Serialization;

namespace Models
{
    public class Account
    {
        public Guid AccountId { get; set; }
        public AccountType AccountType { get; set; }
        public double Balance { get; set; } = 0.00;
        public long CustomerId { get; set; }
        public Customer Customer { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
