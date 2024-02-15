using BankTrackingSystem.Models;

namespace BankTrackingSystem.Data
{
    public interface IApplicantMessagesRespository
    {
        Task<List<ApplicantMessagesModel>> GetAllMessagesAsync();
        Task<List<ApplicantMessagesModel>> GetAllAgainstApplicantId(long applicantId);
        Task<ApplicantMessagesModel?> GetMessageAgainstIdAsync(int messageId);
        Task<ApplicantMessagesModel> AddMessage(ApplicantMessagesModel message);
        Task<List<ApplicantMessagesModel>> GetAllAgainstEmailAddress(string emailAddress);
    }
}
