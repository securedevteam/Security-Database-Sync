using Microsoft.EntityFrameworkCore;
using Secure.SecurityDatabaseSync.DAL.Configurations;
using Secure.SecurityDatabaseSync.DAL.Models;
using System;

namespace Secure.SecurityDatabaseSync.DAL.Contexts
{
    /// <summary>
    /// First application database context.
    /// </summary>
    public class FirstAppContext : DbContext
    {
        /// <summary>
        /// DbSet for FirstModels.
        /// </summary>
        public DbSet<FirstModel> FirstModels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ApplyConfiguration(new FirstModelConfiguration());

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SyncDbFirst;Trusted_Connection=True;");
        }
    }
}
