using MediatR;
using PersonRegistry.Domain.Entities.Persons.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Application.Features.Person.Commands.UpdatePersonBasicInfo
{
    public sealed record UpdatePersonBasicInfoCommand(
        Guid PersonId,
        string Name,
        string Surname,
        GenderType Gender,
        string PersonalNumber,
        DateOnly BirthDate
    ) : IRequest<Unit>;
}
