using BankAppBackend.Models;

namespace BankAppBackend.Service.Interfaces
{
    public interface ITransactionService
    {
        public Transaction AddTransaction(TransactionExtended transaction);
        public IEnumerable<Transaction> GetTransactions();
        public IEnumerable<Transaction> GetTransactionsByAccountId(Guid accountId);

        public Transaction? GetTransactionById(Guid id);
    }
}
