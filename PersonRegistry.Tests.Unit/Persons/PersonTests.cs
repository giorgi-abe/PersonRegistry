using PersonRegistry.Common.Exceptions;
using PersonRegistry.Domain.Entities.Persons;
using PersonRegistry.Domain.Entities.Persons.Enums;
using PersonRegistry.Domain.Entities.Persons.Events;
using PersonRegistry.Domain.Entities.Persons.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Tests.Unit.Persons
{
    public class PersonTests
    {
        private static Person NewPerson()
        {
            return Person.Create(
                new PersonName("Giorgi"),
                new PersonSurname("Smith"),
                GenderType.Male,
                new PersonPersonalNumber("12345678901"),
                new PersonBirthDate(DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-25))));
        }

        [Fact]
        public void Create_Sets_Fields_And_Raises_PersonCreated_And_Increments_Version()
        {
            var p = NewPerson();

            Assert.Equal<uint>(1, p.Version);
            var e = Assert.IsType<PersonCreated>(p.Events.Single());

            Assert.Equal(p.Id.Value, e.PersonId);
            Assert.Equal("Giorgi", e.Name);
            Assert.Equal("Smith", e.Surname);
            Assert.Equal(GenderType.Male, e.Gender);
            Assert.Equal("12345678901", e.PersonalNumber);
        }

        [Fact]
        public void UpdateBasicInfo_Raises_Event_But_Does_Not_Reincrement_Version()
        {
            var p = NewPerson();
            p.ClearEvents(); // make event counting easier but keep Version=1

            p.UpdateBasicInfo(
                new PersonName("Nino"),
                new PersonSurname("Brown"),
                GenderType.Female,
                new PersonPersonalNumber("00000000000"),
                new PersonBirthDate(DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30)))
            );

            Assert.Equal<uint>(1, p.Version); // not incremented again
            var e = Assert.IsType<PersonBasicInfoUpdated>(p.Events.Single());
            Assert.Equal("Nino", e.Name);
            Assert.Equal("Brown", e.Surname);
            Assert.Equal("00000000000", e.PersonalNumber);
            Assert.Equal(GenderType.Female, e.Gender);
        }

        [Fact]
        public void AddPhone_Raises_PhoneAdded()
        {
            var p = NewPerson();
            p.ClearEvents();

            var ph = p.AddPhone(PhoneNumberType.Mobile, new PhoneNumberNumber("+995 599 11 22 33"));

            var ev = Assert.IsType<PhoneAdded>(p.Events.Single());
            Assert.Equal(p.Id.Value, ev.PersonId);
            Assert.Equal(ph.Id.Value, ev.PhoneId);
            Assert.Equal(PhoneNumberType.Mobile, ev.Type);
            Assert.Equal("+995 599 11 22 33", ev.Number);
        }

        [Fact]
        public void UpdatePhone_Raises_PhoneUpdated()
        {
            var p = NewPerson();
            var ph = p.AddPhone(PhoneNumberType.Home, new PhoneNumberNumber("1111"));
            p.ClearEvents();

            p.UpdatePhone(ph.Id, PhoneNumberType.Mobile, new PhoneNumberNumber("2222"));

            var ev = Assert.IsType<PhoneUpdated>(p.Events.Single());
            Assert.Equal(p.Id.Value, ev.PersonId);
            Assert.Equal(ph.Id.Value, ev.PhoneId);
            Assert.Equal(PhoneNumberType.Mobile, ev.NewType);
            Assert.Equal("2222", ev.NewNumber);
        }

        [Fact]
        public void UpdatePhone_NotFound_Throws()
        {
            var p = NewPerson();
            Assert.Throws<DomainException>(() =>
                p.UpdatePhone(Guid.NewGuid(), PhoneNumberType.Mobile, new PhoneNumberNumber("1")));
        }

        [Fact]
        public void RemovePhone_Raises_PhoneRemoved()
        {
            var p = NewPerson();
            var ph = p.AddPhone(PhoneNumberType.Mobile, new PhoneNumberNumber("1111"));
            p.ClearEvents();

            p.RemovePhone(ph.Id);

            var ev = Assert.IsType<PhoneRemoved>(p.Events.Single());
            Assert.Equal(p.Id.Value, ev.PersonId);
            Assert.Equal(ph.Id.Value, ev.PhoneId);
        }

        [Fact]
        public void RemovePhone_NotFound_Throws()
        {
            var p = NewPerson();
            Assert.Throws<DomainException>(() => p.RemovePhone(Guid.NewGuid()));
        }

        [Fact]
        public void ReplacePhones_Produces_Remove_Update_Add_And_Final_Snapshot_Event()
        {
            var p = NewPerson();
            var a = p.AddPhone(PhoneNumberType.Mobile, new PhoneNumberNumber("1111")); // will be updated
            var b = p.AddPhone(PhoneNumberType.Home, new PhoneNumberNumber("2222"));   // will be removed
            p.ClearEvents();

            p.ReplacePhones(new[]
            {
                (PhoneNumberType.Mobile, new PhoneNumberNumber("9999")), // update
                (PhoneNumberType.Office,  new PhoneNumberNumber("3333")),  // add
            });

            // Expect at least: PhoneRemoved(Home), PhoneUpdated(Mobile), PhoneAdded(Work), PhonesReplaced(...)
            Assert.True(p.Events.OfType<PhoneRemoved>().Any());
            Assert.True(p.Events.OfType<PhoneUpdated>().Any());
            Assert.True(p.Events.OfType<PhoneAdded>().Any());
            var final = Assert.IsType<PhonesReplaced>(p.Events.Last());

            // Snapshot contains current phones
            Assert.Equal(p.Id.Value, final.PersonId);
            Assert.Equal(2, final.Phones.Count);
            Assert.Contains(final.Phones, x => x.Type == PhoneNumberType.Mobile && x.Number == "9999");
            Assert.Contains(final.Phones, x => x.Type == PhoneNumberType.Office && x.Number == "3333");
        }

        [Fact]
        public void ReplacePhones_Null_Throws()
        {
            var p = NewPerson();
            Assert.Throws<ArgumentNullException>(() => p.ReplacePhones(null!));
        }

        [Fact]
        public void AddRelation_Raises_Event_And_Prevents_Duplicates_And_Self()
        {
            var p = NewPerson();
            var otherId = Guid.NewGuid();

            var rel = p.AddRelation(otherId, RelationType.Acquaintance);
            var ev = Assert.IsType<PersonRelationAdded>(p.Events.Last());
            Assert.Equal(p.Id.Value, ev.PersonId);
            Assert.Equal(otherId, ev.RelatedPersonId);
            Assert.Equal(RelationType.Acquaintance, ev.Type);

            Assert.Throws<DomainException>(() => p.AddRelation(otherId, RelationType.Acquaintance));
            Assert.Throws<DomainException>(() => p.AddRelation(p.Id, RelationType.Acquaintance));
        }

        [Fact]
        public void RemoveRelation_Raises_Event_And_NotFound_Throws()
        {
            var p = NewPerson();
            var otherId = Guid.NewGuid();
            p.AddRelation(otherId, RelationType.Relative);
            p.ClearEvents();

            p.RemoveRelation(otherId);

            var ev = Assert.IsType<PersonRelationRemoved>(p.Events.Single());
            Assert.Equal(p.Id.Value, ev.PersonId);
            Assert.Equal(otherId, ev.RelatedPersonId);

            Assert.Throws<DomainException>(() => p.RemoveRelation(otherId));
        }
    }
}
