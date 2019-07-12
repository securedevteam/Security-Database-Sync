using Microsoft.EntityFrameworkCore;
using SecurityDatabaseSync.DAL.Models;

namespace SecurityDatabaseSync.DAL
{
    public class ApplicationContextClient : DbContext
    {
        private string _clientIp;

        public DbSet<Pledge> PledgeTable { get; set; }

        public ApplicationContextClient(string clientIp)
        {
            _clientIp = clientIp; 
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(""); // TODO: Вынести настройки в appsettings.json
        }
    }
}
