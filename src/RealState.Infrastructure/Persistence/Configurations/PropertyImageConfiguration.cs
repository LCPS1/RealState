using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealState.Domain.Entities;

namespace RealState.Infrastructure.Persistence.Configurations
{
    public class PropertyImageConfiguration : IEntityTypeConfiguration<PropertyImage>
    {
        public void Configure(EntityTypeBuilder<PropertyImage> builder)
        {
            builder.HasKey(pi => pi.Id);

            builder.Property(pi => pi.FileName)
                .IsRequired()
                .HasMaxLength(255); 

            builder.Property(pi => pi.Description)
                .IsRequired()
                .HasMaxLength(500); 

            builder.Property(pi => pi.File)
                .IsRequired();

            builder.HasOne(pi => pi.Property)
                .WithMany(p => p.Images)
                .HasForeignKey(pi => pi.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}