using Microsoft.EntityFrameworkCore;
using PersonRegistry.Domain.Entities.Persons;
using PersonRegistry.Infrastructure.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using PersonRegistry.Common.Domains.Abstractions;

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

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IAuditableEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType).Property<DateTimeOffset>("CreatedAtUtc");
                    modelBuilder.Entity(entityType.ClrType).Property<DateTimeOffset>("LastModifiedAtUtc");
                }

                if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType).Property<bool>("IsDeleted");
                }
            }

            base.OnModelCreating(modelBuilder);

         
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditableEntity &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            var now = DateTimeOffset.UtcNow;

            foreach (var entry in entries)
            {
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
