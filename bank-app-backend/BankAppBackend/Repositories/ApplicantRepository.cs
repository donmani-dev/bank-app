using BankAppBackend.Models;
using BankAppBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankAppBackend.Repositories
{
    public class ApplicantRepository : IApplicantRepository
    {
        private DatabaseContext _databaseContext;
        public ApplicantRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public Applicant AddApplicant(Applicant applicant)
        {
            _databaseContext.Applicants.Add(applicant);
            _databaseContext.SaveChanges();
            return applicant;
        }

        public Applicant? FindApplicantByCNIC(string cnic)
        {
            return _databaseContext.Applicants.FirstOrDefault(applicant=>applicant.CNIC.Equals(cnic));
        }

        public Applicant? FindApplicantByEmailAddress(string emailAddress)
        {
            return _databaseContext.Applicants.Include(applicant => applicant.Customer).Include(applicant => applicant.Teller).FirstOrDefault(applicant => applicant.EmailAddress.Equals(emailAddress));
        }

        public Applicant? findApplicantById(long applicantId)
        {
           return _databaseContext.Applicants.Include(applicant => applicant.Customer).Include(applicant => applicant.Teller).FirstOrDefault(applicant => applicant.Id.Equals(applicantId));
        }

        public IEnumerable<Applicant> GetApplicants()
        {
            return _databaseContext.Applicants.Include(a => a.Teller).Include(a=>a.Customer);
        }

        public void UpdateApplicant(Applicant applicant) {
            _databaseContext.Applicants.Update(applicant);
            _databaseContext.SaveChanges();
        }

    }
}
