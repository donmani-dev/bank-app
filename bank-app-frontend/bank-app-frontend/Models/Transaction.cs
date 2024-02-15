using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    public enum TransactionType
    {
        CREDIT, TRANSFER
    }
    public class Transaction
    {
        public Guid TransactionId { get; set; }
        public TransactionType TransactionType { get; set; }
        public double Amount { get; set; }
        public DateTime DateTime { get; set; }
        public Guid AccountId { get; set; }
    }

    public class TransactionExtended : Transaction
    {
        public Guid? DepositorAccountId { get; set; }
    }
}
