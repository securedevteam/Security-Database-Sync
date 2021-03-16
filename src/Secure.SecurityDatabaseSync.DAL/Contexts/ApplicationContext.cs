using Microsoft.EntityFrameworkCore;
using Secure.SecurityDatabaseSync.DAL.Configurations;
using Secure.SecurityDatabaseSync.DAL.Models;
using System;

namespace Secure.SecurityDatabaseSync.DAL.Contexts
{
    /// <summary>
    /// Common application database context.
    /// </summary>
    public class ApplicationContext : DbContext
    {
        private readonly string _databaseName;

        /// <summary>
        /// Constructor with params.
        /// </summary>
        /// <param name="databaseName">Database name.</param>
        public ApplicationContext(string databaseName)
        {
            _databaseName = databaseName;
        }

        /// <summary>
        /// DbSet for Commons.
        /// </summary>
        public DbSet<Common> Commons { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ApplyConfiguration(new CommonConfiguration());

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database={_databaseName};Trusted_Connection=True;");
        }
    }
}
