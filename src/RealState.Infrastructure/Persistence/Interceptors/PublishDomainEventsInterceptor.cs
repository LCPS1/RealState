using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RealState.Domain.Common.Models;
using RealState.Domain.Entities;

namespace RealState.Infrastructure.Persistence.Interceptors
{
    public class PublishDomainEventsInterceptor : SaveChangesInterceptor
    {
        private readonly IPublisher _mediator;

        public PublishDomainEventsInterceptor(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            PublishDomainEvents(eventData.Context).GetAwaiter().GetResult();
            return base.SavingChanges(eventData, result);
            
        }


        public async override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await PublishDomainEvents(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }


        private async Task PublishDomainEvents(DbContext? dbcontext)
        {

            if(dbcontext == null)
            {
                return;
            }
            //Get Hold of all various entitites

            var entititesWithDomainEvents =  dbcontext.ChangeTracker
                .Entries<Entity>()  // Changed from IHasDomainEvents to Entity
                .Where(entry => entry.Entity.DomainEvents.Any())
                .Select(entry => entry.Entity)
                .ToList();
            
            /*
            dbcontext.ChangeTracker.Entries<IHasDomainEvents>()
                .Where(entity => entity.Entity.DomainEvents.Any())
                .Select(entity => entity.Entity)
                .ToList();
            //Get HOld of all varsious domain events
*/
            var domainEvents = entititesWithDomainEvents
                .SelectMany(x => x.DomainEvents).ToList();

            //Clear Domain Events

            entititesWithDomainEvents.ForEach(entity => entity.ClearDomainEvents());

            //Publish DOkain Evetns

            foreach(var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }

        }
    }
}