using BankTrackingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BankTrackingSystem.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<ApplicantMessagesModel> Messages { get; set; }

        public string DbPath { get; set; }
        
        public DatabaseContext()
        {
            // Get the project root directory
            var rootPath = Directory.GetCurrentDirectory();

            // Specify the database file name
            var dbName = "bank-tracking-system.db";

            // Combine the root path and the database file name
            DbPath = Path.Combine(rootPath, dbName);
            //var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            //DbPath = Path.Join(path, "bank-tracking-system.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }
}
