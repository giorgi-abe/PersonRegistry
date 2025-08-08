using PersonRegistry.Domain.Entities.Persons.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Application.Features.Person.Queries.GetPersonById
{
    
    public sealed class GetPersonByIdModel
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = default!;
        public string Surname { get; init; } = default!;
        public GenderType Gender { get; init; }
        public string PersonalNumber { get; init; } = default!;
        public DateOnly BirthDate { get; init; }

        public List<GetPersonByIdPhoneModel> Phones { get; init; } = [];
        public List<GetPersonByIdPersonRelationModel> OutgoingRelations { get; init; } = [];
    }

    public sealed class GetPersonByIdPhoneModel
    {
        public Guid Id { get; init; }
        public string Number { get; init; } = default!;
        public PhoneNumberType Type { get; init; }
    }

    public sealed class GetPersonByIdPersonRelationModel
    {
        public Guid Id { get; init; }
        public Guid RelatedPersonId { get; init; }
        public RelationType Type { get; init; }
    }
}
