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

            // Optional: Add additional configurations if required

            base.OnModelCreating(modelBuilder);

         
        }
    }
}
