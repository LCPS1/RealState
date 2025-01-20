using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using RealState.Application.Owners.CreateOwner;
using RealState.Domain.Events;

namespace RealState.Application.Owners.Events
{
    public class CreateOwnerEventHandler : INotificationHandler<OwnerCreated>
    {
        public Task Handle(OwnerCreated notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}