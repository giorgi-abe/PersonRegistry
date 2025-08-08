using MediatR;
using PersonRegistry.Domain.Entities.Persons.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Application.Features.Person.Commands.AddPhoneNumber
{
    public sealed record AddPhoneNumberCommand(
        Guid PersonId,
        PhoneNumberType Type,
        string Number
    ) : IRequest<Guid>;
}
