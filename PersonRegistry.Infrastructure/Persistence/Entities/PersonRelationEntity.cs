using PersonRegistry.Domain.Entities.Persons.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Infrastructure.Persistence.Entities
{
    public class PersonRelationEntity
    {
        public PersonRelationEntity() { }
        public Guid Id { get;set; }
        public Guid PersonId { get;  set; }
        public Guid RelatedPersonId { get;  set; }
        public string Type { get;  set; }
        public bool IsDeleted { get;  set; }

    }
}
