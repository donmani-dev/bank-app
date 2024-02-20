using System;

namespace BankingAzureFunction
{
    public enum AccountStatus
    {
        APPROVED, DENIED, PENDING
    }
    internal class ApplicantMessageModel
    {
        public Guid Id { get; set; }
        public long ApplicantId { get; set; }
        public string? Message { get; set; }
        public string ApplicantEmailAddress { get; set; }

        public AccountStatus accountStatus { get; set; }
    }
}
