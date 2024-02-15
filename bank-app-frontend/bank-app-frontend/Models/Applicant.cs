using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public enum AccountType
    {
        CURRENT, SAVING, BUSINESS
    }
    public enum AccountStatus
    {
        APPROVED, DENIED, PENDING
    }
    public class Applicant
    {
        public long Id { get; set; }
        public string ApplicateName { get; set; }
        public string Address { get; set; }
        public string CNIC { get; set; }
        public string EmailAddress { get; set; }
        public DateOnly DOB { get; set; } // Change DateOnly to DateTime
        public AccountType AccountType { get; set; }
        public AccountStatus AccountStatus { get; set; } = AccountStatus.PENDING;
        public long? TellerId { get; set; }
        public Teller? Teller { get; set; } // Assuming TellerViewModel is another ViewModel
        public Customer? Customer { get; set; } 
    }
}
