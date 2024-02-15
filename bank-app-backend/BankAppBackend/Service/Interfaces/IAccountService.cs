using BankAppBackend.Models;

namespace BankAppBackend.Service.Interfaces
{
    public interface IAccountService
    {
        public Account CreateNewAccount(Customer customer);

        public Account GetAccountById(Guid id);
        public List<Account> GetAccountsAgainstCustomerId(long customerId);

        public Account GetAccountAgainstCustomerId(long customerId);

    }
}
