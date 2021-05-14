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
        /// <summary>
        /// Contructor with params.
        /// </summary>
        /// <param name="options">Database context options.</param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
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
    }
}
