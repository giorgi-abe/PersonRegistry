using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonRegistry.Domain.Entities.Persons.Enums;
using MediatR;
namespace PersonRegistry.Application.Features.Person.Commands.AddPerson
{
    public sealed record AddPersonCommand(
        string Name,
        string Surname,
        GenderType Gender,
        string PersonalNumber,
        DateOnly BirthDate,
        List<PhoneInput> Phones,
        List<RelationInput> Relations
    ) : IRequest<Guid>{};

    public sealed record PhoneInput(PhoneNumberType Type, string Number);

    public sealed record RelationInput(Guid RelatedPersonId, RelationType Type);
}
