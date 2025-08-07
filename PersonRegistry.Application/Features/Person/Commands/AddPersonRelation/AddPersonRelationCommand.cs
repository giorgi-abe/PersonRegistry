using MediatR;
using PersonRegistry.Domain.Entities.Persons.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Application.Features.Person.Commands.AddPersonRelation
{

    public sealed record AddPersonRelationCommand(
        Guid PersonId,
        Guid RelatedPersonId,
        RelationType RelationType
    ) : IRequest<Unit>;
}
