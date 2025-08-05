using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonRegistry.Domain.Entities.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Infrastructure.Persistence.Configurations
{
    public sealed class PersonRelationConfiguration : IEntityTypeConfiguration<PersonRelation>
    {
        public void Configure(EntityTypeBuilder<PersonRelation> b)
        {
            b.ToTable("PersonRelations");

            // Composite key
            b.HasKey(r => new { r.PersonId, r.RelatedPersonId, r.Type });

            b.Property(r => r.PersonId).IsRequired();
            b.Property(r => r.RelatedPersonId).IsRequired();

            b.Property(r => r.Type)
             .HasConversion<string>()
             .HasMaxLength(20)
             .IsRequired();

            // Owner (outgoing) relations cascade on delete of owner
            b.HasOne<Person>()
             .WithMany(p => p.OutgoingRelations)
             .HasForeignKey(r => r.PersonId)
             .OnDelete(DeleteBehavior.Cascade);

            // Incoming relations must NOT cascade (avoid cycles)
            b.HasOne<Person>()
             .WithMany(p => p.IncomingRelations)
             .HasForeignKey(r => r.RelatedPersonId)
             .OnDelete(DeleteBehavior.NoAction);

            // Optional helper index
            b.HasIndex(r => new { r.PersonId, r.Type });
        }
    }
}
