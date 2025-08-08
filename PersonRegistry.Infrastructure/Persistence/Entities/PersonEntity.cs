using PersonRegistry.Domain.Entities.Persons;
using PersonRegistry.Domain.Entities.Persons.Enums;
using PersonRegistry.Domain.Entities.Persons.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Infrastructure.Persistence.Entities
{
    public class PersonEntity
    {
        public PersonEntity() { }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get;  set; }
        public string PersonalNumber { get;  set; }
        public DateOnly BirthDate { get;  set; }

        public uint Version { get;  set; }
        public DateTimeOffset CreatedAtUtc { get;  set; }
        public DateTimeOffset LastModifiedAtUtc { get;  set; }
        public bool IsDeleted { get;  set; }


        public List<PhoneNumberEntity> PhoneNumbers { get; set; }

        public List<PersonRelationEntity> OutgoingRelations { get; set; }
        public ICollection<PersonRelationEntity> IncomingRelations { get; set; }
    }
}
