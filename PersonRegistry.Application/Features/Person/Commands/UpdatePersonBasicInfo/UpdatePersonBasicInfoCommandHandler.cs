using MediatR;
using PersonRegistry.Application.Repositories.Aggregates;
using PersonRegistry.Domain.Entities.Persons.Enums;
using PersonRegistry.Domain.Entities.Persons.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PersonRegistry.Application.Features.Person.Commands.UpdatePersonBasicInfo
{

    public sealed class UpdatePersonBasicInfoCommandHandler : IRequestHandler<UpdatePersonBasicInfoCommand, Unit>
    {
        private readonly IPersonRepository _personRepository;

        public UpdatePersonBasicInfoCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Unit> Handle(UpdatePersonBasicInfoCommand request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(request.PersonId,cancellationToken);
            if (person is null)
                throw new KeyNotFoundException($"Person with ID {request.PersonId} not found.");

            person.UpdateBasicInfo(
                request.Name,
                request.Surname,
                request.Gender,
                request.PersonalNumber,
                request.BirthDate
            );
         
            _personRepository.UpdateAsync(person);

            return Unit.Value;
        }
    }
}
