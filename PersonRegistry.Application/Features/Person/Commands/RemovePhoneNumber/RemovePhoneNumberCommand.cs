using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Application.Features.Person.Commands.RemovePhoneNumber
{
    public sealed record RemovePhoneNumberCommand(
        Guid PersonId,
        Guid PhoneNumberId
    ) : IRequest<Unit>;
}
