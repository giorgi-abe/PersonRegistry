using MediatR;
using PersonRegistry.Application.DTOs;
using PersonRegistry.Domain.Entities.Persons.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Application.Features.Person.Queries.SearchPeople
{
    public sealed record SearchPeopleQuery(
    string? Name,
    string? Surname,
    string? PersonalNumber,
    GenderType? Gender,
    DateOnly? BirthDate,
    int Page,
    int PageSize
) : IRequest<PagedResult<PersonListItemDto>>;
}
