using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonRegistry.Domain.Entities.Persons;

namespace PersonRegistry.Application.Repositories.Aggregates
{
    public interface IBasketRepository : IRepository<Person>
    {
    }
}
