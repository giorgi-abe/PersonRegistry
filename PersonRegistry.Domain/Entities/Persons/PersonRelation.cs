using PersonRegistry.Common.Domains;
using PersonRegistry.Domain.Entities.Persons.Enums;
using PersonRegistry.Domain.Entities.Persons.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Domain.Entities.Persons
{
    public sealed class PersonRelation : Entity<PersonRelation>
    {
        public Guid PersonId { get; private set; }
        public Guid RelatedPersonId { get; private set; }
        public RelationType Type { get; private set; }

        private PersonRelation() { }
        private PersonRelation(Guid personId, Guid relatedPersonId, RelationType type)
        {
            if (personId == Guid.Empty) throw new ArgumentOutOfRangeException(nameof(personId));
            if (relatedPersonId == Guid.Empty) throw new ArgumentOutOfRangeException(nameof(relatedPersonId));
            if (personId == relatedPersonId) throw new ArgumentException("Cannot relate a person to themselves.");

            PersonId = personId;
            RelatedPersonId = relatedPersonId;
            Type = type;
        }

        public static PersonRelation Create(Guid personId, Guid relatedPersonId, RelationType type)
            => new(personId, relatedPersonId, type);
    }
}
