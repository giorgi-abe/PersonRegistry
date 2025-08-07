using PersonRegistry.Domain.Entities.Persons.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Application.Features.Person.Queries.SearchPeople
{
    
    public sealed class SearchPeopleModel
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = default!;
        public string Surname { get; init; } = default!;
        public string PersonalNumber { get; init; } = default!;
        public GenderType Gender { get; init; }
        public DateOnly BirthDate { get; init; }

        public List<SearchPeoplePhoneModel> Phones { get; init; } = new();
    }

    public sealed class SearchPeoplePhoneModel
    {
        public PhoneNumberType Type { get; init; }
        public string Number { get; init; } = default!;
    }
}
