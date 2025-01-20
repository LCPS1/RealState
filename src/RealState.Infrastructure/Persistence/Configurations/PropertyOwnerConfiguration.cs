using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RealState.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace RealState.Infrastructure.Persistence.Configurations
{
    public class PropertyOwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
             builder.HasKey(o => o.Id);

            builder.Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.Photo)
                .IsRequired(false); 

            builder.OwnsOne(o => o.Address, a =>
            {
                a.Property(a => a.Street)
                    .IsRequired()
                    .HasMaxLength(255);

                a.Property(a => a.City)
                    .IsRequired()
                    .HasMaxLength(100);

                a.Property(a => a.ZipCode)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            builder.Property(o => o.Birthday)
                .IsRequired(true); 

            builder.HasMany(o => o.Properties)
                .WithOne(p => p.Owner)
                .HasForeignKey(p => p.OwnerId);

        }
    }
}