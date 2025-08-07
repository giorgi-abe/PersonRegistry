using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PersonRegistry.Application.Repositories.Aggregates;
using Entities = PersonRegistry.Domain.Entities.Persons;

namespace PersonRegistry.Application.Features.Person.Commands.AddPerson
{
    public class AddPersonCommandHandler :IRequestHandler<AddPersonCommand,Guid>
    {
        private readonly IPersonRepository _personRepository;
        public async Task<Guid> Handle(AddPersonCommand request, CancellationToken cancellationToken)
        {
                var person = new Entities.Person(
                    request.Name,
                    request.Surname,
                    request.Gender,
                    request.PersonalNumber,
                    request.BirthDate
                );

                await _personRepository.AddAsync(person, cancellationToken);
                return person.Id;
        }
    }
}
