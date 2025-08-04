using PersonRegistry.Common.Events;
using PersonRegistry.Common.Events.Abstraction;
using PersonRegistry.Domain.Entities.Persons.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Domain.Entities.Persons.Events
{
    public sealed record PersonRelationAdded(
        Guid PersonId,          
        Guid RelatedPersonId,   
        RelationType Type,
        DateTimeOffset? OccurredAtUtc
    ) : DomainEvent(PersonId, OccurredAtUtc ?? DateTimeOffset.UtcNow);

    public sealed record PersonRelationRemoved(
        Guid PersonId,
        Guid RelatedPersonId,
        RelationType Type,
        DateTimeOffset? OccurredAtUtc
    ) : DomainEvent(PersonId,OccurredAtUtc ?? DateTimeOffset.UtcNow);

  
}
