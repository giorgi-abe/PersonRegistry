using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonRegistry.Domain.Entities.Persons;
using PersonRegistry.Domain.Entities.Persons.ValueObjects;
using PersonRegistry.Infrastructure.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Infrastructure.Persistence.Configurations
{
    public sealed class PhoneNumberConfiguration : IEntityTypeConfiguration<PhoneNumberEntity>
    {
        public void Configure(EntityTypeBuilder<PhoneNumberEntity> b)
        {
            b.ToTable("PhoneNumbers");
            b.HasKey(p => p.Id);

            b.Property(p => p.PersonId).IsRequired();

            b.Property(p => p.Type)
             .HasConversion<string>()
             .HasMaxLength(10)
             .IsRequired();

            b.Property(p => p.Number)
             .HasColumnName("Number")
             .HasMaxLength(50)
             .IsRequired();

            b.HasOne<PersonEntity>()
             .WithMany(p => p.PhoneNumbers)
             .HasForeignKey(p => p.PersonId)
             .OnDelete(DeleteBehavior.Cascade);



            b.HasIndex(p => new { p.PersonId, p.Type, p.Number })
             .IsUnique()
             .HasFilter("[IsDeleted] = 0");


        }
    }
}
