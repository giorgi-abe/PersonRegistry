using PersonRegistry.Common.Domains.Abstractions;
using PersonRegistry.Common.Events.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Common.Events
{
    public record DomainEvent : IDomainEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTimeOffset OccurredOnUtc { get; }
        public Guid AggregateId { get; }
        public string EventType { get; set; }

        protected DomainEvent(Guid aggregateId, DateTimeOffset occurredOnUtc)
        {
            AggregateId = aggregateId != Guid.Empty ? aggregateId : throw new ArgumentNullException(nameof(aggregateId));
            OccurredOnUtc = occurredOnUtc;
            EventType = GetEventType(this);
        }
        public static string GetEventType(IDomainEvent @event) =>
            GetEventType(@event.GetType());

        public static string GetEventType(Type eventType)
        {
            return eventType.Name;
        }
    }
}
