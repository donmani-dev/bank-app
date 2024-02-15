using BankAppBackend.Exceptions;
using BankAppBackend.Models;
using BankAppBackend.Repositories.Interfaces;
using BankAppBackend.Service.Interfaces;

namespace BankAppBackend.Service
{
    public class TransactionService : ITransactionService
    {

        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountService _accountService;

        public TransactionService(ITransactionRepository transactionRepository,IAccountService accountService) {
            this._transactionRepository = transactionRepository;
            this._accountService = accountService;
        }
        public Transaction AddTransaction(TransactionExtended transaction)
        {
            if(transaction.TransactionType.Equals(TransactionType.CREDIT))
            {
                return processCreditTransaction(transaction);
            }
            else
            {
                Account senderAccount = this._accountService.GetAccountById(transaction.AccountId);
                Account receiverAccount = this._accountService.GetAccountById(transaction.DepositorAccountId.Value);
                return processTransferTransaction(transaction, senderAccount, receiverAccount);
            }

        }

        private Transaction processCreditTransaction(Transaction transaction)
        {
            Account account = this._accountService.GetAccountById(transaction.AccountId);
            transaction.Account = account;
            transaction.Amount = ValidateAmountAndReturn(transaction);
            transaction.DateTime = DateTime.Now;
            return this._transactionRepository.AddTransaction(transaction);
        }

        private Transaction processTransferTransaction(TransactionExtended transactionExtended, Account senderAccount, Account receiverAccount)
        {
            Transaction senderTransaction = new Transaction();
            senderTransaction.TransactionType = transactionExtended.TransactionType;
            senderTransaction.Amount = transactionExtended.Amount;
            senderTransaction.Account = senderAccount;
            senderTransaction.Amount = ValidateAmountAndReturn(senderTransaction);
            senderTransaction.DateTime = DateTime.Now;
            
            this._transactionRepository.AddTransaction(senderTransaction);

            Transaction receiverTransaction = new Transaction();
            receiverTransaction.TransactionType = TransactionType.CREDIT;
            receiverTransaction.Account = receiverAccount;
            receiverTransaction.Amount = transactionExtended.Amount;
            receiverTransaction.Amount = ValidateAmountAndReturn(receiverTransaction);
            receiverTransaction.DateTime = DateTime.Now;
            
            this._transactionRepository.AddTransaction(receiverTransaction);
            
            return senderTransaction;
        }

        public Transaction? GetTransactionById(Guid id)
        {
            Transaction transaction = this._transactionRepository.GetTransactionById(id);
            if(transaction == null) {
                throw new EntityNotFound($"Transaction not found against transaction id {id}");
            }

            return transaction;
        }

        public double ValidateAmountAndReturn(Transaction txn)
        {
            if (txn.TransactionType == TransactionType.CREDIT)
            {
                if (txn.Amount > 0)
                {
                    txn.Account.Balance = txn.Account.Balance + txn.Amount;
                }
                else
                {
                    throw new InvalidOperationException("Invalid Operation.");
                }

            }
            else if(txn.TransactionType == TransactionType.TRANSFER)
            {
                if (txn.Account?.Balance >= txn.Amount)
                {

                    txn.Account.Balance = txn.Account.Balance - txn.Amount;
                }
                else
                {
                    throw new InvalidOperationException("Insufficient account balance");
                }

            }
            return txn.Amount;

        }

        public IEnumerable<Transaction> GetTransactions()
        {
           return this._transactionRepository.GetTransactions();
            
        }

        public IEnumerable<Transaction> GetTransactionsByAccountId(Guid accountId)
        {
            return _transactionRepository.GetTransactionsByAccountId(accountId);
        }
    }
}
