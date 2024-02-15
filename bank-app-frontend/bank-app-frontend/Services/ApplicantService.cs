using bank;
using Models;

namespace Services
{
    public class ApplicantService
    {
        private readonly HttpClientService httpClientService;
        public ApplicantService(HttpClientService httpClientService) {
            this.httpClientService = httpClientService;
        }

        public async Task<List<Applicant>> GetApplicantsList()
        {
            string endPointUrl = AppRoutes.APPLICANTS;
            var applicants = await httpClientService.SendGetRequest< List<Applicant>>(endPointUrl, typeof(List<Applicant>));
            return applicants;
        }


        public async Task<Transaction> AddTransaction(TransactionExtended transaction)
        {
            string endPointUrl = AppRoutes.TRANSACTION;
            var responseTransaction = await httpClientService.PostDataRequest<TransactionExtended, Transaction>(endPointUrl, transaction, typeof(Transaction));
            return responseTransaction;
        }

        public async Task<Applicant> UpdateApplicantList(Applicant applicantDetails)
        {
            string endPointUrl = $"{AppRoutes.APPLICANTS}";
            return await httpClientService.PostDataRequest<Applicant, Applicant>(endPointUrl, applicantDetails, typeof(Applicant));
        }

        public async Task<Applicant> GetApplicantById(long applicantId)
        {
            string endPointUrl = $"{AppRoutes.APPLICANTS}/{applicantId}";
            return await httpClientService.SendGetRequest<Applicant>(endPointUrl, typeof(Applicant));
        }

        public async Task<Applicant> GetApplicantByEmailAddress(string emailAddress)
        {
            string endPointUrl = $"{AppRoutes.APPLICANT_EMAIL}/{emailAddress}";
            return await httpClientService.SendGetRequest<Applicant>(endPointUrl, typeof(Applicant));
        }
    }
}
