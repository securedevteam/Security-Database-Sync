using Microsoft.EntityFrameworkCore;
using Secure.SecurityDatabaseSync.DAL.Configurations;
using Secure.SecurityDatabaseSync.DAL.Models;
using System;

namespace Secure.SecurityDatabaseSync.DAL.Contexts
{
    /// <summary>
    /// Second application database context.
    /// </summary>
    public class SecondAppContext : DbContext
    {
        /// <summary>
        /// DbSet for SecondModels.
        /// </summary>
        public DbSet<SecondModel> SecondModels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ApplyConfiguration(new SecondModelConfiguration());

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SyncDbSecond;Trusted_Connection=True;");
        }
    }
}
