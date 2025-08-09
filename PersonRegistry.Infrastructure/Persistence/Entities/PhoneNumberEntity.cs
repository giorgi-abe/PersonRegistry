using PersonRegistry.Domain.Entities.Persons.Enums;
using PersonRegistry.Domain.Entities.Persons.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Infrastructure.Persistence.Entities
{
    public class PhoneNumberEntity : DatabaseEntity
    {
        public PhoneNumberEntity() { }
        public Guid Id { get; set; }
        public Guid PersonId { get;  set; }
        public string Type { get;  set; }
        public string Number { get;  set; }
    }
}
