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
    public sealed class PhoneNumberConfiguration : IEntityTypeConfiguration<PhoneNumber>
    {
        public void Configure(EntityTypeBuilder<PhoneNumber> b)
        {
            b.ToTable("PhoneNumbers");
            b.HasKey(p => p.Id);

            b.Property(p => p.PersonId).IsRequired();

            b.Property(p => p.Type)
             .HasConversion<string>()
             .HasMaxLength(10)
             .IsRequired();

            b.OwnsOne(p => p.Number, nb =>
            {
                nb.Property(v => v.Value)
                  .HasColumnName("Number")
                  .HasMaxLength(50)
                  .IsRequired();
            });

            b.HasOne<Person>()
             .WithMany(p => p.PhoneNumbers)
             .HasForeignKey(p => p.PersonId)
             .OnDelete(DeleteBehavior.Cascade);

          
            b.HasIndex(p => new
            {
                p.PersonId,
                p.Type,
                Number = EF.Property<string>(p, "Number") 
            })
             .IsUnique();

    
        }
    }
}
