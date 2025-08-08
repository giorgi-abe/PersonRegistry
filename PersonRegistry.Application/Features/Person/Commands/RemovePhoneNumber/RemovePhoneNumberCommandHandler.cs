using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PersonRegistry.Application.Repositories;
using PersonRegistry.Application.Repositories.Aggregates;
using PersonRegistry.Common.Domains;

namespace PersonRegistry.Application.Features.Person.Commands.RemovePhoneNumber
{
    public sealed class RemovePhoneNumberCommandHandler : IRequestHandler<RemovePhoneNumberCommand,Unit>
    {
        private readonly IPersonRepository _personRepository;

        public RemovePhoneNumberCommandHandler(
            IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Unit> Handle(RemovePhoneNumberCommand request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(request.PersonId, cancellationToken)
                         ?? throw new Exception("Person not found.");

            person.RemovePhone(request.PhoneNumberId);


            return Unit.Value;
        }
    }
}
