using bank;
using Models;

namespace Services
{
    public class CustomerService
    {
        private readonly HttpClientService httpClientService;
        public CustomerService(HttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }
        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            string endPointUrl = AppRoutes.UPDATE_CUSTOMER;
            return await httpClientService.PutDataRequest<Customer, Customer>(endPointUrl, customer, typeof(Customer));
        }
        public async Task<Account> GetAccountByCustomerId(long customerId)
        {
            string endPointUrl = $"{AppRoutes.CUSTOMER}/customerAccount/{customerId}";
            return await httpClientService.SendGetRequest<Account>(endPointUrl, typeof(Account));
        }
    }
}
