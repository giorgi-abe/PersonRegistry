using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonRegistry.Application.Repositories.Aggregates;
using PersonRegistry.Application.Repositories.DTOs;
using PersonRegistry.Common.Models;
using PersonRegistry.Domain.Entities.Persons;
using PersonRegistry.Infrastructure.Persistence.Entities;
using PersonRegistry.Infrastructure.Persistence.MappingProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PersonRegistry.Infrastructure.Persistence.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PersonRegistryDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IdentityMap _identity;
        private readonly PersonAssembler _asm;
        public PersonRepository(PersonRegistryDbContext dbContext, IMapper mapper, IdentityMap identity, PersonAssembler asm)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _identity = identity;
            _asm = asm;
        }

        public async Task AddAsync(Person person, CancellationToken cancellationToken)
        {
            var personDto = _mapper.Map<PersonEntity>(person);
            await _dbContext.People.AddAsync(personDto, cancellationToken);
        }

        public async Task<Person?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.People
                .Include(p => p.IncomingRelations)
                .Include(p => p.OutgoingRelations)
                .Include(p => p.PhoneNumbers)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken)
                 ?? throw new KeyNotFoundException($"Person {id} not found.");

            var d = _mapper.Map<Person>(entity);
            _identity.Attach(d,entity);
            return d;
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
            if (_identity.TryGetEntity(person, out PersonEntity personEntity)) {
                _asm.Apply(person, personEntity);
                await Task.CompletedTask;

            }else
            { 
                    var personDto = _mapper.Map<PersonEntity>(person);
                _dbContext.People.Update(personDto);
                await Task.CompletedTask;
            }
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
                query = query.Where(p => EF.Functions.Like(p.Name, $"%{request.Name}%"));

            if (!string.IsNullOrWhiteSpace(request.Surname))
                query = query.Where(p => EF.Functions.Like(p.Surname, $"%{request.Surname}%"));

            if (!string.IsNullOrWhiteSpace(request.PersonalNumber))
                query = query.Where(p => EF.Functions.Like(p.PersonalNumber, $"%{request.PersonalNumber}%"));

            if (request.Gender.HasValue)
                query = query.Where(p => p.Gender == request.Gender.Value.ToString());

            var totalCount = await query.CountAsync();

            var itemsDto = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var items = _mapper.Map<List<Person>>(itemsDto);
            return new PagedResult<Person>(items, totalCount, request.Page, request.PageSize);
        }

    }
}
