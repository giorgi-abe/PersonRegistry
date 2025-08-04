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
    public sealed record PhoneAdded(
        Guid PersonId,
        Guid PhoneId,
        PhoneNumberType Type,
        string Number,
        DateTimeOffset? OccurredAtUtc
    ) : DomainEvent(PersonId, OccurredAtUtc ?? DateTimeOffset.UtcNow);

    public sealed record PhoneUpdated(
        Guid PersonId,
        Guid PhoneId,
        PhoneNumberType OldType,
        string OldNumber,
        PhoneNumberType NewType,
        string NewNumber,
        DateTimeOffset? OccurredAtUtc
    ) : DomainEvent(PersonId, OccurredAtUtc ?? DateTimeOffset.UtcNow);

    public sealed record PhoneRemoved(
        Guid PersonId,
        Guid PhoneId,
        DateTimeOffset? OccurredAtUtc
    ) : DomainEvent(PersonId, OccurredAtUtc ?? DateTimeOffset.UtcNow);

    // If your API always sends the full list and you do a clear + re-add
    // you can publish one summary event instead of many add/remove events.
    public sealed record PhonesReplaced(
        Guid PersonId,
        IReadOnlyList<PhoneSnapshot> Phones,
        DateTimeOffset? OccurredAtUtc
    ) : DomainEvent(PersonId,OccurredAtUtc ?? DateTimeOffset.UtcNow);

    public sealed record PhoneSnapshot(
        Guid PhoneId,
        PhoneNumberType Type,
        string Number
    );
}
