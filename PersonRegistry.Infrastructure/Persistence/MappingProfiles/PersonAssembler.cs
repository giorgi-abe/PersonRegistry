using PersonRegistry.Common.Domains;
using PersonRegistry.Domain.Entities.Persons;
using PersonRegistry.Domain.Entities.Persons.Enums;
using PersonRegistry.Domain.Entities.Persons.ValueObjects;
using PersonRegistry.Infrastructure.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Infrastructure.Persistence.MappingProfiles
{
    public sealed class PersonAssembler
    {
        // Maps values from the domain Person to the persistence PersonEntity.
        public void Apply(Person d, PersonEntity e)
        {
            e.Name = d.Name.Value;
            e.Surname = d.Surname.Value;
            e.Gender = d.Gender.ToString();
            e.PersonalNumber = d.PersonalNumber.Value;
            e.BirthDate = d.BirthDate.Value;
            e.Version = d.Version;

            SyncPhones(d, e);
            SyncRelations(d, e);
        }

        // Syncs phone numbers: updates existing, removes missing, adds new.
        private static void SyncPhones(Person d, PersonEntity e)
        {
            var existing = e.PhoneNumbers.ToDictionary(x => x.Id, x => x);

            var domainIds = d.PhoneNumbers.Select(p => (Guid)p.Id).ToHashSet();
            e.PhoneNumbers.RemoveAll(x => !domainIds.Contains(x.Id));
            foreach (var dp in d.PhoneNumbers)
            {
                if (existing.TryGetValue((Guid)dp.Id, out var ep))
                {
                    ep.Type = dp.Type.ToString();
                    ep.Number = dp.Number.Value;
                }
                else
                {
                    e.PhoneNumbers.Add(new PhoneNumberEntity
                    {
                        Id = dp.Id.Value,
                        PersonId = e.Id,
                        Type = dp.Type.ToString(),
                        Number = dp.Number.Value
                    });
                }
            }
        }

        // Syncs outgoing relations: updates existing, removes missing, adds new.
        private static void SyncRelations(Person d, PersonEntity e)
        {
            var existing = e.OutgoingRelations.ToDictionary(r => r.Id);
            var keepIds = d.OutgoingRelations.Select(r => (Guid)r.Id).ToHashSet();

            e.OutgoingRelations.RemoveAll(r => !keepIds.Contains(r.Id));

            foreach (var dr in d.OutgoingRelations)
            {
                if (existing.TryGetValue((Guid)dr.Id, out var er))
                {
                    er.Type = dr.Type.ToString();
                    er.RelatedPersonId = (Guid)dr.RelatedPersonId;
                }
                else
                {
                    e.OutgoingRelations.Add(new PersonRelationEntity
                    {
                        Id = (Guid)dr.Id,
                        PersonId = e.Id, // current person as "owner"
                        RelatedPersonId = (Guid)dr.RelatedPersonId,
                        Type = dr.Type.ToString()
                    });
                }
            }
        }

    }
}
