using BankAppBackend.Models;

namespace BankAppBackend.Repositories.Interfaces
{
    public interface ITellerRepository
    {
        public Teller? GetTellerById(long tellerId);
        public Teller RegisterTeller(Teller teller);
        public Teller? GetTellerByEmailAddress(string emailAddress);
        public Teller? GetTellerDetailsByLoginCredentials(string emailAddress, string password);
    }
}
