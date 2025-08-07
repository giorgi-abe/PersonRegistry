using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonRegistry.Application.DTOs;
using PersonRegistry.Domain.Entities.Persons;

namespace PersonRegistry.Application.Repositories.Aggregates
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<PagedResult<Person>> SearchAsync(PersonSearchRequest request, CancellationToken cancellationToken);

    }
}
