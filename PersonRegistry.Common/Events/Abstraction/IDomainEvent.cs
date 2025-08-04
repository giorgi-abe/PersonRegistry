using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Common.Events.Abstraction
{
    public interface IDomainEvent
    {
        Guid Id { get; }

        DateTimeOffset OccurredOnUtc { get; }

        Guid AggregateId { get; }
        public string EventType { get; set; }
    }
}
