using Microsoft.EntityFrameworkCore;
using SecurityDatabaseSync.DAL.Models;

namespace SecurityDatabaseSync.DAL
{
    public class ApplicationContextServer : DbContext
    {
        public DbSet<Pledge> PledgeTable { get; set; }

        public ApplicationContextServer()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(""); // TODO: Вынести настройки в appsettings.json
        }
    }
}
