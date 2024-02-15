using bank;
using Models;

namespace Services
{
    public class TellerService
    {
        private HttpClientService httpClientService;
        public TellerService(HttpClientService httpClientService) {
            this.httpClientService = httpClientService;
        }

        public async Task UpdateApplicantStatusAsync(long applicantId, AccountStatus accountStatus, long tellerId)
        {
            string endPointUrl = $"{AppRoutes.CHANGE_TELLER_STATUS}/{applicantId}";
            await httpClientService.PutDataRequest<object, object>(endPointUrl, new { AccountStatus = accountStatus, TellerId = tellerId }, typeof(object));
        }
    }
}
