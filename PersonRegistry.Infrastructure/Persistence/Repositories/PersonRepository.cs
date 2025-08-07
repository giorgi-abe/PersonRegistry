using Microsoft.EntityFrameworkCore;
using PersonRegistry.Application.Repositories.Aggregates;
using PersonRegistry.Domain.Entities.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PersonRegistry.Common.Models;
using PersonRegistry.Application.Repositories.DTOs;

namespace PersonRegistry.Infrastructure.Persistence.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PersonRegistryDbContext _dbContext;

        public PersonRepository(PersonRegistryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Person person, CancellationToken cancellationToken)
        {
            await _dbContext.People.AddAsync(person, cancellationToken);
        }

        public async Task<Person?> GetByIdAsync(Guid id, CancellationToken cancellationToken    )
        {
            var entity = await _dbContext.People
                .Include(p => p.IncomingRelations)
                .Include(p => p.OutgoingRelations)
                .Include(p => p.PhoneNumbers)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

            return entity;
        }

        public async Task<IEnumerable<Person>> GetAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.People.AnyAsync(p => p.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(Person person)
        {
            _dbContext.People.Update(person);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _dbContext.People.FindAsync(id);
            if (entity is not null)
                _dbContext.People.Remove(entity);
        }

        public async Task<PagedResult<Person>> SearchAsync(PersonSearchRequest request, CancellationToken cancellationToken)
        {

            var query = _dbContext.People
                .Include(p => p.PhoneNumbers)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Name))
                query = query.Where(p => EF.Functions.Like(p.Name.Value, $"%{request.Name}%"));

            if (!string.IsNullOrWhiteSpace(request.Surname))
                query = query.Where(p => EF.Functions.Like(p.Surname.Value, $"%{request.Surname}%"));

            if (!string.IsNullOrWhiteSpace(request.PersonalNumber))
                query = query.Where(p => EF.Functions.Like(p.PersonalNumber.Value, $"%{request.PersonalNumber}%"));

            if (request.Gender.HasValue)
                query = query.Where(p => p.Gender == request.Gender);

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<Person>(items, totalCount, request.Page, request.PageSize);
        }

    }
}
