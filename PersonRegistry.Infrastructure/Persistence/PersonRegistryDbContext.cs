using Microsoft.EntityFrameworkCore;
using PersonRegistry.Common.Domains.Abstractions;
using PersonRegistry.Domain.Entities.Persons;
using PersonRegistry.Infrastructure.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Infrastructure.Persistence
{
    public sealed class PersonRegistryDbContext : DbContext
    {
        public PersonRegistryDbContext(DbContextOptions<PersonRegistryDbContext> options)
            : base(options) { }

        public DbSet<PersonEntity> People { get; set; }
        
        public DbSet<PhoneNumberEntity> PhoneNumbers { get; set; }
        public DbSet<PersonRelationEntity> PersonRelations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply Fluent API configurations from the current assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Add a global filter so soft-deleted entities (IsDeleted = true) are excluded from queries.
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(parameter, nameof(ISoftDeletable.IsDeleted));
                    var condition = Expression.Equal(property, Expression.Constant(false));
                    var lambda = Expression.Lambda(condition, parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }

            base.OnModelCreating(modelBuilder);

         
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Set auditing fields (CreatedAtUtc, LastModifiedAtUtc) for added/modified auditable entities.
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditableEntity &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            var now = DateTimeOffset.UtcNow;

            foreach (var entry in entries)
            {
                // If CreatedAtUtc is missing, treat it as a new entity
                if (entry.Property("CreatedAtUtc").CurrentValue is null)
                    entry.State = EntityState.Added;

                if (entry.State == EntityState.Added)
                    entry.Property("CreatedAtUtc").CurrentValue = now;

                entry.Property("LastModifiedAtUtc").CurrentValue = now;
            }

            var deletables = ChangeTracker.Entries()
                .Where(e => e.Entity is ISoftDeletable && e.State == EntityState.Deleted);

            foreach (var entry in deletables)
            {
                entry.State = EntityState.Modified;
                entry.Property("IsDeleted").CurrentValue = true;
                entry.Property("LastModifiedAtUtc").CurrentValue = now;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
