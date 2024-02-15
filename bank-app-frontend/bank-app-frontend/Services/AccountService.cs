using bank;
using Models;

namespace Services
{
    public class AccountService
    {
        private readonly HttpClientService httpClientService;
        public AccountService(HttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }
        public async Task<List<Transaction>> GetAccountTransactionsByAccountId(Guid accountId)
        {
            string endPointUrl = $"{AppRoutes.TRANSACTION}/accountTransactions/{accountId}";
            return await httpClientService.SendGetRequest<List<Transaction>>(endPointUrl, typeof(List<Transaction>));
        }
    }
}
