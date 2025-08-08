using MediatR;
using PersonRegistry.Application.Repositories.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Application.Features.Person.Commands.RemovePersonRelation
{
    public sealed class RemovePersonRelationCommandHandler : IRequestHandler<RemovePersonRelationCommand, Unit>
    {
        private readonly IPersonRepository _personRepository;

        public RemovePersonRelationCommandHandler(
            IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Unit> Handle(RemovePersonRelationCommand request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(request.PersonId, cancellationToken);

            if (person is null)
                throw new KeyNotFoundException($"Person with ID {request.PersonId} not found.");

            person.RemoveRelation(request.RelatedPersonId);

            await _personRepository.UpdateAsync(person);

            return Unit.Value;
        }
    }
}
