using Microsoft.EntityFrameworkCore;
using SecurityDatabaseSync.DAL.Models;

namespace SecurityDatabaseSync.DAL
{
    public class ApplicationContext : DbContext
    {
        private readonly string _databaseName;

        public DbSet<Pledge> PledgeTable { get; set; }
        public DbSet<TestModel> TestModelTable { get; set; }

        public ApplicationContext(string databaseName)
        {
            _databaseName = databaseName; 
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database={_databaseName};Trusted_Connection=True;MultipleActiveResultSets=true"); // TODO: Вынести настройки в appsettings.json
        }
    }
}
