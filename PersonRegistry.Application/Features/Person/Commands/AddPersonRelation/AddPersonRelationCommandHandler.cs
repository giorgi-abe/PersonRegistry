using MediatR;
using PersonRegistry.Application.Features.Person.Commands.AddPerson;
using PersonRegistry.Application.Repositories.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonRegistry.Common.Exceptions;

namespace PersonRegistry.Application.Features.Person.Commands.AddPersonRelation
{
    public class AddPersonRelationCommandHandler : IRequestHandler<AddPersonRelationCommand, Unit>
    {
        private readonly IPersonRepository _personRepository;

        public AddPersonRelationCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Unit> Handle(AddPersonRelationCommand request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(request.PersonId, cancellationToken);
            if (person == null)
                throw new NotFoundException($"Person with ID {request.PersonId} not found");

            person.AddRelation(request.RelatedPersonId, request.RelationType);

            await _personRepository.UpdateAsync(person);

            return Unit.Value;
        }
    }
}
