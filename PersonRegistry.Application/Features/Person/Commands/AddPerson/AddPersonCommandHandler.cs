using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PersonRegistry.Application.Repositories.Aggregates;

namespace PersonRegistry.Application.Features.Person.Commands.AddPerson
{
    public class AddPersonCommandHandler :IRequestHandler<AddPersonCommand,Guid>
    {
        private readonly IPersonRepository _ipersonRepository;
        public Task<Guid> Handle(AddPersonCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
