using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonRegistry.Domain.Entities.Persons;
using PersonRegistry.Infrastructure.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Infrastructure.Persistence.Configurations
{
    public sealed class PersonRelationConfiguration : IEntityTypeConfiguration<PersonRelationEntity>
    {
        public void Configure(EntityTypeBuilder<PersonRelationEntity> b)
        {
            b.ToTable("PersonRelations");

            b.HasKey(r => r.Id);

            b.Property(r => r.PersonId).IsRequired();
            b.Property(r => r.RelatedPersonId).IsRequired();

            b.Property(r => r.Type)
             .HasConversion<string>()
             .HasMaxLength(20)
             .IsRequired();

            b.HasOne<PersonEntity>()
             .WithMany(p => p.OutgoingRelations)
             .HasForeignKey(r => r.PersonId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne<PersonEntity>()
             .WithMany(p => p.IncomingRelations)
             .HasForeignKey(r => r.RelatedPersonId)
             .OnDelete(DeleteBehavior.NoAction);

            b.HasIndex(p => new { p.PersonId, p.RelatedPersonId, p.Type })
             .IsUnique()
             .HasFilter("[IsDeleted] = 0");
        }
    }
}
