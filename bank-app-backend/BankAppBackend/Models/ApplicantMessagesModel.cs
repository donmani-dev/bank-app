using BankAppBackend.Models;

namespace BankTrackingSystem.Models
{
    public class ApplicantMessagesModel
    {
        public Guid Id { get; set; }
        public long ApplicantId { get; set; }
        public string? Message { get; set; }
        public string ApplicantEmailAddress { get; set; }

        public AccountStatus accountStatus { get; set; }    
    }
}
