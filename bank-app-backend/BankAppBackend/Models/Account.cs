using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BankAppBackend.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AccountId {  get; set; }
        public AccountType AccountType { get; set; }
        public double Balance { get; set; } = 0.00;
        public long CustomerId { get; set; }
        public Customer Customer { get; set; }

        [JsonIgnore]
        public ICollection<Transaction> Transactions { get; set; }
    }
}
