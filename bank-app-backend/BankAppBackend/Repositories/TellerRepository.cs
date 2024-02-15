using BankAppBackend.Models;
using BankAppBackend.Repositories.Interfaces;

namespace BankAppBackend.Repositories
{
    public class TellerRepository : ITellerRepository
    {
        private readonly DatabaseContext _databaseContext;
        public TellerRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public Teller? GetTellerById(long tellerId)
        {
            return _databaseContext.Tellers.FirstOrDefault(t => t.Id.Equals(tellerId));
        }

        public IEnumerable<Teller> GetAllTellers() {
            return _databaseContext.Tellers;
        }

        public Teller RegisterTeller(Teller teller)
        {
            _databaseContext.Tellers.Add(teller);
            _databaseContext.SaveChanges();
            return teller;
        }

        public Teller? GetTellerByEmailAddress(string emailAddress)
        {
            return _databaseContext.Tellers.FirstOrDefault(t => t.EmailAddress.Equals(emailAddress));

        }

        public Teller? GetTellerDetailsByLoginCredentials(string emailAddress, string password)
        {
            return _databaseContext.Tellers.FirstOrDefault(teller=>teller.EmailAddress.Equals(emailAddress) &&  teller.Password.Equals(password));
        }
    }
}
