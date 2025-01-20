using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using RealState.Domain.Events;


namespace RealState.Application.Properties.Events
{
    public class CreatePropertyEventHandler : INotificationHandler<PropertyBuildingCreated>
    {
        public Task Handle(PropertyBuildingCreated notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}