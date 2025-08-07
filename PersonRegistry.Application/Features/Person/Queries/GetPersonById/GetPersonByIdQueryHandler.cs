using MediatR;
using PersonRegistry.Application.Repositories.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace PersonRegistry.Application.Features.Person.Queries.GetPersonById
{
    public sealed class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, GetPersonByIdModel>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public GetPersonByIdQueryHandler(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<GetPersonByIdModel> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByIdAsync(request.PersonId,cancellationToken);

            if (person is null)
                throw new KeyNotFoundException($"Person with ID {request.PersonId} not found.");

            return _mapper.Map<GetPersonByIdModel>(person);
        }
    }
}
