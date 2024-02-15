using BankAppBackend.Models;
using BankAppBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankAppBackend.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DatabaseContext _databaseContext;
        public CustomerRepository(DatabaseContext databaseContext) {
            _databaseContext = databaseContext;
        }

        public void CreateAccount(Account account)
        {
            _databaseContext.Accounts.Add(account);
            _databaseContext.SaveChanges();
        }

        public void CreateCustomer(Customer customer)
        {
            _databaseContext.Customers.Add(customer);
            _databaseContext.SaveChanges();
        }

        public List<Customer> GetAllCustomers()
        {
            return _databaseContext.Customers.Include(customer=>customer.Applicant).Include(customer=>customer.Accounts).ToList();
        }

        public void UpdateCustomer(Customer customer)
        {
            var existingEntity = _databaseContext.Customers.Local.FirstOrDefault(c => c.CustomerId == customer.CustomerId);
            if (existingEntity != null)
            {
                _databaseContext.Entry(existingEntity).State = EntityState.Detached;
            }
            _databaseContext.Customers.Update(customer);
            _databaseContext.SaveChanges();
        }
    }
}
