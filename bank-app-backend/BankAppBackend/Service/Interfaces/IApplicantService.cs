using BankAppBackend.Models;

namespace BankAppBackend.Service.Interfaces
{
    public interface IApplicantService
    {
        public Applicant AddApplicant(Applicant applicant);
        public List<Applicant> GetAllApplicantList();
        public Applicant GetApplicantById(long applicantId);
        public void UpdateApplicantStatus(long applicantId, AccountStatus accountStatus, Teller teller);
        public Applicant? GetApplicantDetailsFromCredentials(string emailAddress, string password);
        public Applicant GetApplicantByEmail(string applicantEmail);
    }
}
