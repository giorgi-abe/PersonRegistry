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

    public sealed class PersonConfiguration : IEntityTypeConfiguration<PersonEntity>
    {
        public void Configure(EntityTypeBuilder<PersonEntity> b)
        {
            b.ToTable("Persons");
            b.HasKey(p => p.Id);

            // Gender (enum)
            b.Property(p => p.Gender)
             .HasConversion<string>()
             .HasMaxLength(10)
             .IsRequired();


            b.Property(v => v.Name)
                  .HasColumnName("Name")
                  .HasMaxLength(50)
                  .IsRequired();

           // b.HasIndex(v => v.Name);



            b.Property(v => v.Surname)
                  .HasColumnName("Surname")
                  .HasMaxLength(50)
                  .IsRequired();

            //b.HasIndex(v => v.Surname);


            b.Property(v => v.PersonalNumber)
               .HasColumnName("PersonalNumber")
               .HasMaxLength(11)
               .IsRequired();

           // b.HasIndex(v => v.PersonalNumber).IsUnique();



            b.Property(v => v.BirthDate)
              .HasColumnName("BirthDate")
              .IsRequired();

            b.Property(v => v.IsDeleted)
              .HasColumnName("IsDeleted")
              .IsRequired();

            b.Property(v => v.Version)
              .HasColumnName("Version")
              .HasDefaultValue(1)
              .IsRequired();

            b.Property(v => v.LastModifiedAtUtc)
              .HasColumnName("LastModifiedAtUtc");

            b.Property(v => v.CreatedAtUtc)
             .HasColumnName("CreatedAtUtc");

            // Backing-field navigations (if you have private lists)
            b.Navigation(p => p.PhoneNumbers).UsePropertyAccessMode(PropertyAccessMode.Field);
            b.Navigation(p => p.OutgoingRelations).UsePropertyAccessMode(PropertyAccessMode.Field);
            b.Navigation(p => p.IncomingRelations).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
