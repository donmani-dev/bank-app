using BankAppBackend.Exceptions;
using BankAppBackend.Models;
using BankAppBackend.Repositories.Interfaces;
using BankAppBackend.Service.Interfaces;
using BankTrackingSystem.Models;
using Newtonsoft.Json;
using System.Text;

namespace BankAppBackend.Service
{
    public class ApplicantService : IApplicantService
    {
        private IApplicantRepository applicantRepository;
        private IRedisMessagePublisherService redisMessagePublisherService;
        private ICustomerService customerService;
        private ILogger<ApplicantService> logger;
        public ApplicantService(IApplicantRepository applicantRepository, IRedisMessagePublisherService redisMessagePublisherService, ICustomerService customerService, ILogger<ApplicantService> logger)
        {
            this.applicantRepository = applicantRepository;
            this.redisMessagePublisherService = redisMessagePublisherService;
            this.customerService = customerService;
            this.logger = logger;
        }

        public Applicant AddApplicant(Applicant applicant)
        {
            if (applicantRepository.FindApplicantByCNIC(applicant.CNIC) != null)
            {
                throw new EntityAlreadyExist($"Applicant already exist with CNIC number : {applicant.CNIC}");
            }
            else if (applicantRepository.FindApplicantByEmailAddress(applicant.EmailAddress) != null)
            {
                throw new EntityAlreadyExist($"Applicant already exist with email address : {applicant.EmailAddress}");
            }
            applicant.AccountStatus = AccountStatus.PENDING;
            applicantRepository.AddApplicant(applicant);
            return applicant;
        }

        public List<Applicant> GetAllApplicantList()
        {
            return applicantRepository.GetApplicants().ToList();
        }

        public Applicant GetApplicantByEmail(string applicantEmail)
        {
            Applicant? applicant = applicantRepository.FindApplicantByEmailAddress(applicantEmail);
            if (applicant == null)
            {
                throw new EntityNotFound($"Applicant not found with applicant email address :{applicantEmail}");
            }
            return applicant;
        }

        public Applicant GetApplicantById(long applicantId)
        {
            Applicant? applicant = applicantRepository.findApplicantById(applicantId);
            if (applicant == null)
            {
                throw new EntityNotFound($"Applicant not found with applicant id :{applicantId}");
            }
            return applicant;
        }

        public Applicant? GetApplicantDetailsFromCredentials(string emailAddress, string password)
        {
            Applicant? applicant = applicantRepository.FindApplicantByEmailAddress(emailAddress);
            if (applicant == null)
            {
                throw new EntityNotFound($"User not found with email address {emailAddress}");
            }
            else if (applicant.Customer == null)
            {
                throw new EntityNotFound($"Your applicant request is not approved by bank officials yet");
            }
            else if (applicant.Customer.Password.Equals(password))
            {
                return applicant;
            }

            return null;
        }

        public async void UpdateApplicantStatus(long applicantId, AccountStatus accountStatus, Teller teller)
        {
            Applicant? applicant = GetApplicantById(applicantId);
            if (applicant == null)
            {
                throw new EntityNotFound($"Applicant with id {applicantId} not found");
            }

            if (customerService.CheckIfCustomerExistAgainstApplicantId(applicantId))
            {
                throw new EntityAlreadyExist($"Customer already exist against this applicant id {applicantId}");
            }

            applicant.Teller = teller;
            applicant.AccountStatus = accountStatus;

            applicantRepository.UpdateApplicant(applicant);
            if (applicant.AccountStatus.Equals(AccountStatus.APPROVED))
            {
                customerService.CreateCustomerAndAccount(applicant);
            }

            ApplicantMessagesModel applicantMessageModel = new ApplicantMessagesModel();
            applicantMessageModel.ApplicantId = applicant.Id;
            applicantMessageModel.accountStatus = accountStatus;
            applicantMessageModel.ApplicantEmailAddress = applicant.EmailAddress;
            applicantMessageModel.Message = $"Dear Applicant {applicant.ApplicateName}, your status has been updated to {accountStatus}";
            await sendMailToApplicant(applicantMessageModel);
        }

        private async Task sendMailToApplicant(ApplicantMessagesModel applicantMessagesModel)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = "http://localhost:7088/api/EmailFunction";
                    string jsonRequestData = JsonConvert.SerializeObject(applicantMessagesModel);
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
                    request.Headers.Add("Accept", "application/json");
                    request.Content = new StringContent(jsonRequestData, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        logger.LogDebug("Email send successfully");
                    }
                    else
                    {
                        logger.LogError($"Error occurred with status code {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError($"Exception occurred " + ex.Message);
                }
            }
        }
    }
}
