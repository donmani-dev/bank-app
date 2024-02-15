using BankTrackingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BankTrackingSystem.Data
{
    public class ApplicantMessagesRespository : IApplicantMessagesRespository
    {
        private readonly DatabaseContext _databaseContext;
        public ApplicantMessagesRespository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public async Task<ApplicantMessagesModel> AddMessage(ApplicantMessagesModel message)
        {
            _databaseContext.Messages.Add(message);
            await _databaseContext.SaveChangesAsync();
            return message;
        }

        public async Task<List<ApplicantMessagesModel>> GetAllAgainstApplicantId(long applicantId)
        {
            return await _databaseContext.Messages.Where(message => message.ApplicantId.Equals(applicantId)).ToListAsync();
        }

        public async Task<List<ApplicantMessagesModel>> GetAllAgainstEmailAddress(string emailAddress)
        {
            return await _databaseContext.Messages.Where(message => message.ApplicantEmailAddress.Equals(emailAddress)).ToListAsync();
        }

        public async Task<List<ApplicantMessagesModel>> GetAllMessagesAsync()
        {
            return await _databaseContext.Messages.ToListAsync();
        }

        public async Task<ApplicantMessagesModel?> GetMessageAgainstIdAsync(int messageId)
        {
            return await _databaseContext.Messages.FirstOrDefaultAsync(message => message.Id.ToString() == messageId.ToString());
        }
    }
}
