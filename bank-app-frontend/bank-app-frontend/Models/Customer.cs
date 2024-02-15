namespace Models
{
    public class Customer
    {
        public long CustomerId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public Applicant Applicant { get; set; }
        public long ApplicantId { get; set; }
    }
}
