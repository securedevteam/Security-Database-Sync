using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Secure.SecurityDatabaseSync.DAL.Constants;
using Secure.SecurityDatabaseSync.DAL.Models;
using System;

namespace Secure.SecurityDatabaseSync.DAL.Configurations
{
    /// <summary>
    /// EF Configuration for FirstModel entity.
    /// </summary>
    public class FirstModelConfiguration : IEntityTypeConfiguration<FirstModel>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<FirstModel> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(Table.FirstModels, Schema.Test)
                .HasKey(firstModel => firstModel.Id);

            builder.Property(firstModel => firstModel.Name)
                .IsRequired()
                .HasMaxLength(SqlConfiguration.SqlMaxLengthMedium);

            builder.Property(firstModel => firstModel.Description)
                .HasMaxLength(SqlConfiguration.SqlMaxLengthLong);
        }
    }
}
