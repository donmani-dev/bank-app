using BankAppBackend.Models;

namespace BankAppBackend.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        public Transaction AddTransaction(Transaction transaction);
        public IEnumerable<Transaction> GetTransactions();
        public IEnumerable<Transaction> GetTransactionsByAccountId(Guid accountId);
        public Transaction? GetTransactionById(Guid id);
    }
}
