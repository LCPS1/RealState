using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RealState.Domain.Entities;
using RealState.Domain.Interfaces;
using RealState.Infrastructure.Persistence.Interceptors;
//using RealState.Domain.Property;

namespace RealState.Infrastructure.Persistence
{
    public class RealStateDbContext : DbContext
    {
        private readonly PublishDomainEventsInterceptor _publishDomainEvent;
        public RealStateDbContext(DbContextOptions<RealStateDbContext> options, PublishDomainEventsInterceptor publishDomainEvent)
            : base(options)
        {
            _publishDomainEvent = publishDomainEvent;
        }

        public DbSet<PropertyBuilding> Properties { get; set; }
        public DbSet<Owner> PropertyOwners { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<PropertyTrace> PropertyTraces { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Ignore<List<IDomainEvent>>()
                .ApplyConfigurationsFromAssembly(typeof(RealStateDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_publishDomainEvent);
            base.OnConfiguring(optionsBuilder);
        }

    }
}