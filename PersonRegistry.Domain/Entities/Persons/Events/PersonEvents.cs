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
    public sealed record PersonCreated(
        Guid PersonId,
        string Name,
        string Surname,
        GenderType Gender,
        string PersonalNumber,
        DateOnly BirthDate,
        DateTimeOffset? OccurredAtUtc
    ) : DomainEvent(PersonId, OccurredAtUtc ?? DateTimeOffset.UtcNow);

    public sealed record PersonBasicInfoUpdated(
        Guid PersonId,
        string Name,
        string Surname,
        GenderType Gender,
        string PersonalNumber,
        DateOnly BirthDate,
        DateTimeOffset? OccurredAtUtc
    ) : DomainEvent(PersonId, OccurredAtUtc ?? DateTimeOffset.UtcNow);

    public sealed record PersonDeleted(
        Guid PersonId,
        DateTimeOffset? OccurredAtUtc
    ) : DomainEvent(PersonId, OccurredAtUtc ?? DateTimeOffset.UtcNow);
    
}
