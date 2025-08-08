using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PersonRegistry.Application.Repositories;
using PersonRegistry.Application.Repositories.Aggregates;
using PersonRegistry.Common.Domains;
using PersonRegistry.Domain.Entities.Persons.ValueObjects;

namespace PersonRegistry.Application.Features.Person.Commands.AddPhoneNumber
{
    public sealed class AddPhoneNumberCommandHandler : IRequestHandler<AddPhoneNumberCommand, Guid>
    {
        private readonly IPersonRepository _personRepository;

        public AddPhoneNumberCommandHandler(
            IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Guid> Handle(AddPhoneNumberCommand request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(request.PersonId,cancellationToken)
                         ?? throw new Exception("Person not found");

            var number = new PhoneNumberNumber(request.Number);
            var phone = person.AddPhone(request.Type, number);

            await _personRepository.UpdateAsync(person);
            return phone.Id;
        }
    }
}
