using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealState.Domain.Entities;

namespace RealState.Infrastructure.Persistence.Configurations
{
    public class PropertyConfiguration : IEntityTypeConfiguration<PropertyBuilding>
    {
        public void Configure(EntityTypeBuilder<PropertyBuilding> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(255);
           
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

            builder.OwnsOne(p => p.Price, m =>
            {
                m.Property(m => m.Amount)
                    .IsRequired()
                    .HasPrecision(18, 2);

                m.Property(m => m.Currency)
                    .IsRequired()
                    .HasMaxLength(3);
            });

            builder.Property(p => p.CodeInternal)
                .IsRequired()
                .HasMaxLength(50); 
                
            builder.Property(p => p.Year)
                .IsRequired();


            //Relationships
            builder.HasOne(p => p.Owner)
                .WithMany(o => o.Properties)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasMany(p => p.Traces)
                .WithOne(pt => pt.PropertyBuilding)
                .HasForeignKey(pt => pt.PropertyId);

            builder.HasMany(p => p.Images) 
                .WithOne(pi => pi.Property) 
                .HasForeignKey(pi => pi.PropertyId); 
        }
    }
}