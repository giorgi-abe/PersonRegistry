using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Application.Features.Person.Commands.DeletePerson
{
    public sealed record DeletePersonCommand(Guid PersonId) : IRequest<Unit>;

}
