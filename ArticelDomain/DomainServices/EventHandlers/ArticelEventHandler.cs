using ArticelDomain.DomainEvents;
using System;
using Zaaby.DDD.Abstractions.Domain;

namespace ArticelDomain.DomainServices.EventHandlers
{
    public class ArticelEventHandler : IDomainEventHandler<NewArticelCreateDomainEvent>
    {
        public ArticelEventHandler()
        {
        }

        public void Handle(NewArticelCreateDomainEvent domainEvent)
        {
            throw new NotImplementedException();
        }
    }
}
