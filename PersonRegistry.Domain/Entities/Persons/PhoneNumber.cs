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
    public sealed class PhoneNumber : Entity<PhoneNumber>
    {
        public Guid PersonId { get; private set; }
        public PhoneNumberType Type { get; private set; }
        public PhoneNumberNumber Number { get; private set; }

        private PhoneNumber() { } 

        private PhoneNumber(Guid personId, PhoneNumberType type, PhoneNumberNumber number)
        {
            if (personId == Guid.Empty) throw new ArgumentOutOfRangeException(nameof(personId));
            PersonId = personId;
            Type = type;
            Number = number ?? throw new ArgumentNullException(nameof(number));
        }

        public static PhoneNumber Create(Guid personId, PhoneNumberType type, PhoneNumberNumber number)
            => new(personId, type, number);

        public void ChangeType(PhoneNumberType type) => Type = type;

        public void ChangeNumber(PhoneNumberNumber number)
            => Number = number ?? throw new ArgumentNullException(nameof(number));

    }
}
