using MediatR;
using PersonRegistry.Domain.Entities.Persons.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonRegistry.Common.Models;

namespace PersonRegistry.Application.Features.Person.Queries.SearchPeople
{
    public sealed record SearchPeopleQuery(
        string? Name,
        string? Surname,
        string? PersonalNumber,
        GenderType? Gender,
        DateOnly? BirthDate,
        int? Page,
        int? PageSize
    ) : IRequest<PagedResult<SearchPeopleModel>>;
}
