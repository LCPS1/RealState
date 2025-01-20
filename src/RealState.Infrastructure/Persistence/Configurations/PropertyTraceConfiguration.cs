using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RealState.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealState.Infrastructure.Persistence.Configurations
{
    public class PropertyTraceConfiguration : IEntityTypeConfiguration<PropertyTrace>
    {
        public void Configure(EntityTypeBuilder<PropertyTrace> builder)
        {
             builder.HasKey(pt => pt.Id);

            builder.Property(pt => pt.DateSale)
                .IsRequired();

            builder.Property(pt => pt.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(pt => pt.Value)
                .IsRequired()
                .HasPrecision(5, 2);

            builder.OwnsOne(p => p.Tax, m =>
            {
                m.Property(m => m.Percentage)
                    .IsRequired()
                    .HasPrecision(18, 2);

                m.Property(m => m.Type)
                    .IsRequired()
                    .HasMaxLength(3);
            });

            builder.HasOne(pt => pt.PropertyBuilding)
                .WithMany(p => p.Traces) 
                .HasForeignKey(pt => pt.PropertyId)
                .OnDelete(DeleteBehavior.Restrict); 

            }
    }
}