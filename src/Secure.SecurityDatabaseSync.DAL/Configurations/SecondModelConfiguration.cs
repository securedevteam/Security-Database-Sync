using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Secure.SecurityDatabaseSync.DAL.Constants;
using Secure.SecurityDatabaseSync.DAL.Models;
using System;

namespace Secure.SecurityDatabaseSync.DAL.Configurations
{
    /// <summary>
    /// EF Configuration for SecondModel entity.
    /// </summary>
    public class SecondModelConfiguration : IEntityTypeConfiguration<SecondModel>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<SecondModel> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(Table.SecondModels, Schema.Test)
                .HasKey(secondModel => secondModel.Id);

            builder.Property(secondModel => secondModel.Name)
                .IsRequired()
                .HasMaxLength(SqlConfiguration.SqlMaxLengthMedium);
        }
    }
}
