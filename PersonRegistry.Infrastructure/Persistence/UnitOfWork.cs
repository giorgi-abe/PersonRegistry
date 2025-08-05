using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonRegistry.Application.Repositories;

namespace PersonRegistry.Infrastructure.Persistence
{
    public class UnitOfWork(PersonRegistryDbContext dbContext) : IUnitOfWork
    {
        private readonly PersonRegistryDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Save changes to the database
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose() => _dbContext.Dispose();
    }
}
