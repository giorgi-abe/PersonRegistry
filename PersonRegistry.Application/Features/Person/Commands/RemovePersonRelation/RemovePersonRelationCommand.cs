using MediatR;
using PersonRegistry.Domain.Entities.Persons.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Application.Features.Person.Commands.RemovePersonRelation
{
    public sealed record RemovePersonRelationCommand(
        Guid PersonId,
        Guid RelatedPersonId,
        RelationType RelationType
    ) : IRequest<Unit>;
}
