using MediatR;
using Microsoft.Extensions.Logging;
using PersonRegistry.Application.Repositories;
using PersonRegistry.Application.Repositories.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Application.Features.Person.Commands.DeletePerson
{
    public class DeletePersonCommandHandler
    {
        private readonly IPersonRepository _personRepository;

        public DeletePersonCommandHandler(
            IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            

            await _personRepository.DeleteAsync(request.PersonId);


            return Unit.Value;
        }
    }
}
