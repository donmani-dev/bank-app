using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components;

namespace BankAppBackend.Models
{
    public enum TransactionType
    {
        CREDIT,TRANSFER
    }
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TransactionId { get; set; }
        public TransactionType TransactionType { get; set; }
        public double Amount{ get; set; }
        public DateTime DateTime { get; set; }
        public Guid AccountId { get; set; }
        [JsonIgnore]
        public Account? Account { get; set; }


    }

    public class TransactionExtended : Transaction
    {
        public Guid? DepositorAccountId { get; set; }
    }
}
