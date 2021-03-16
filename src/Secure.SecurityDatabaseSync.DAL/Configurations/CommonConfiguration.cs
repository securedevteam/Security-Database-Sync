using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Secure.SecurityDatabaseSync.DAL.Constants;
using Secure.SecurityDatabaseSync.DAL.Models;
using System;

namespace Secure.SecurityDatabaseSync.DAL.Configurations
{
    /// <summary>
    /// EF Configuration for Common entity.
    /// </summary>
    public class CommonConfiguration : IEntityTypeConfiguration<Common>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Common> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(Table.Commons, Schema.Test)
                .HasKey(common => common.Id);

            builder.Property(common => common.Code)
                .IsRequired()
                .HasMaxLength(SqlConfiguration.SqlMaxLengthShort);

            builder.Property(common => common.InternalNumber)
                .IsRequired()
                .HasMaxLength(SqlConfiguration.SqlMaxLengthShort);

            builder.Property(common => common.Name)
                .IsRequired()
                .HasMaxLength(SqlConfiguration.SqlMaxLengthMedium);
        }
    }
}
