using BankAppBackend.Models;
using BankAppBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankAppBackend.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DatabaseContext _databaseContext;  
        public AccountRepository(DatabaseContext databaseContext) {
            this._databaseContext = databaseContext;
        }

        public Account CreateAccount(Account account)
        {
            _databaseContext.Accounts.Add(account);
            _databaseContext.SaveChanges();
            return account;
        }

        public Account GetAccountAgainstCustomerId(long customerId)
        {
            return _databaseContext.Accounts.Include(account => account.Customer).ThenInclude(account=>account.Applicant).FirstOrDefault(account => account.Customer.CustomerId.Equals(customerId));

        }

        public Account? GetAccountById(Guid id)
        {
            return _databaseContext.Accounts.FirstOrDefault(account => account.AccountId.Equals(id));
        }

        public List<Account> GetAccountsAgainstCustomerId(long customerId)
        {
            return _databaseContext.Accounts.Where(accounts=>accounts.CustomerId.Equals(customerId)).ToList(); 
        }
    }
}
