using BankAppBackend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BankAppBackend.Models
{
    public class DatabaseContext :DbContext
    {
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Teller> Tellers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> transactions { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // to store unique CNIC
            modelBuilder.Entity<Applicant>(applicant =>
            {
                applicant.HasIndex(customer => customer.CNIC).IsUnique(true);
                applicant.HasIndex(customer => customer.EmailAddress).IsUnique(true);  
            });

            // teller - applicants one-to-many relation
            modelBuilder.Entity<Teller>(tellerEntity =>
            {
                tellerEntity.HasIndex(teller=>teller.EmailAddress).IsUnique(true);
                tellerEntity.HasMany(teller => teller.Applicants)
                .WithOne(applicant => applicant.Teller)
                .HasForeignKey(applicant => applicant.TellerId);
            });

            // account - transactions one-to-many relation
            modelBuilder.Entity<Account>(acntEntity =>
            {
                acntEntity.HasMany(acnt=> acnt.Transactions)
                .WithOne(txn=> txn.Account)
                .HasForeignKey(txn => txn.AccountId);
            });

            // applicant - customer one-to-one relation
            modelBuilder.Entity<Applicant>(applicantEntity =>
            {
                applicantEntity.HasOne(applicant => applicant.Customer)
                .WithOne(customer => customer.Applicant)
                .HasForeignKey<Customer>(customer => customer.ApplicantId);
            });

            // customer - accounts one-to-many relation
            modelBuilder.Entity<Customer>(customerEntity =>
            {
                customerEntity.HasMany(customer => customer.Accounts)
                .WithOne(account => account.Customer)
                .HasForeignKey(account => account.CustomerId);
            });
        }
    }
}
