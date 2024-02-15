using BankAppBackend.Models;

namespace BankAppBackend.Service.Interfaces
{
    public interface ICustomerService
    {
        public Customer CreateCustomerAndAccount(Applicant applicant);
        public Customer UpdateExistingCustomer(Customer customer);
        public Customer? FindCustomerByApplicantId(long applicantId);
        public Customer? FindCustomerById(long customerId);
        public List<Customer> GetAllCustomers();
        public bool CheckIfCustomerExistAgainstApplicantId(long applicantId);
    }
}
