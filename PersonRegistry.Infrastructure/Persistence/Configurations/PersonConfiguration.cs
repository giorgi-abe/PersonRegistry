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

    public sealed class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> b)
        {
            b.ToTable("Persons");
            b.HasKey(p => p.Id);

            // VOs as owned types → single columns
            b.OwnsOne(p => p.Name, nb =>
            {
                nb.Property(v => v.Value)
                  .HasColumnName("Name")
                  .HasMaxLength(50)
                  .IsRequired();
            });

            b.OwnsOne(p => p.Surname, nb =>
            {
                nb.Property(v => v.Value)
                  .HasColumnName("Surname")
                  .HasMaxLength(50)
                  .IsRequired();
            });

            b.OwnsOne(p => p.PersonalNumber, nb =>
            {
                nb.Property(v => v.Value)
                  .HasColumnName("PersonalNumber")
                  .HasMaxLength(11)
                  .IsRequired();
            });

            b.OwnsOne(p => p.BirthDate, nb =>
            {
                nb.Property(v => v.Value)
                  .HasColumnName("BirthDate")
                  .IsRequired();
            });

            // Enums as strings
            b.Property(p => p.Gender)
             .HasConversion<string>()
             .HasMaxLength(10)
             .IsRequired();

            // Unique personal number
            b.HasIndex("PersonalNumber").IsUnique();
            b.HasIndex("Name");
            b.HasIndex("Surname");

            // Collections use field access (backing fields)
            b.Navigation(p => p.PhoneNumbers).UsePropertyAccessMode(PropertyAccessMode.Field);
            b.Navigation(p => p.OutgoingRelations).UsePropertyAccessMode(PropertyAccessMode.Field);
            b.Navigation(p => p.IncomingRelations).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
