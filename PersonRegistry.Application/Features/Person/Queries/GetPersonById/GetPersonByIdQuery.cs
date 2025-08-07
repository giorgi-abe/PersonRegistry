using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Application.Features.Person.Queries.GetPersonById
{
    public sealed record GetPersonByIdQuery(Guid PersonId)
    : IRequest<PersonDto>;
}
