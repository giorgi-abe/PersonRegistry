using AutoMapper;
using MediatR;
using PersonRegistry.Application.Repositories.Aggregates;
using PersonRegistry.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonRegistry.Application.Repositories.DTOs;

namespace PersonRegistry.Application.Features.Person.Queries.SearchPeople
{
    public sealed class SearchPeopleQueryHandler : IRequestHandler<SearchPeopleQuery, PagedResult<PersonListItemDto>>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public SearchPeopleQueryHandler(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<PersonListItemDto>> Handle(SearchPeopleQuery request, CancellationToken cancellationToken)
        {
            var result = await _personRepository.SearchAsync(
                new PersonSearchRequest
                {
                    Name = request.Name,
                    Surname = request.Surname,
                    PersonalNumber = request.PersonalNumber,
                    Gender = request.Gender,
                    Page = request.Page ?? 0,
                    PageSize = request.Page ?? 0
                },
                cancellationToken
            );

            return new PagedResult<PersonListItemDto>(
                result.TotalCount,
                result.Items.Select(p => _mapper.Map<PersonListItemDto>(p)).ToList()
            );
        }
    }
}
