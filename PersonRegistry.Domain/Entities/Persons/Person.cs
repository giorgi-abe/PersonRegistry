using PersonRegistry.Common.Domains;
using PersonRegistry.Common.Exceptions;
using PersonRegistry.Domain.Entities.Persons.Enums;
using PersonRegistry.Domain.Entities.Persons.Events;
using PersonRegistry.Domain.Entities.Persons.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Domain.Entities.Persons
{
    public sealed class Person : AggregateRoot<Person>
    {
        public PersonName Name { get; private set; }
        public PersonSurname Surname { get; private set; }
        public GenderType Gender { get; private set; }
        public PersonPersonalNumber PersonalNumber { get; private set; }
        public PersonBirthDate BirthDate { get; private set; }

        private readonly List<PhoneNumber> _phoneNumbers = new();

        private readonly List<PersonRelation> _outgoingRelations = new();
        private readonly List<PersonRelation> _incomingRelations = new(); 

        public List<PhoneNumber> PhoneNumbers => _phoneNumbers;
        public List<PersonRelation> OutgoingRelations => _outgoingRelations;
        public List<PersonRelation> IncomingRelations => _incomingRelations;
        
        //Creation
        private Person() { }
        private Person(PersonName name, PersonSurname surname, GenderType gender,
               PersonPersonalNumber personalNumber, PersonBirthDate birthDate, Id<Person>? id):base(id)
        {
            Name = name;
            Surname = surname;
            Gender = gender;
            PersonalNumber = personalNumber;
            BirthDate = birthDate;

            AddEvent(new PersonCreated(
                Id,
                Name.Value,
                Surname.Value,
                Gender,
                PersonalNumber.Value,
                BirthDate.Value,
                DateTimeOffset.UtcNow));

        }
        public static Person Create(PersonName name, PersonSurname surname, GenderType gender,
                                PersonPersonalNumber personalNumber, PersonBirthDate birthDate, Id<Person>? id = null)
        => new(name, surname, gender, personalNumber, birthDate,id);

        //Update
        public void UpdateBasicInfo(
            PersonName name,
            PersonSurname surname,
            GenderType gender,
            PersonPersonalNumber personalNumber,
            PersonBirthDate birthDate)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Surname = surname ?? throw new ArgumentNullException(nameof(surname));
            Gender = gender;
            PersonalNumber = personalNumber ?? throw new ArgumentNullException(nameof(personalNumber));
            BirthDate = birthDate ?? throw new ArgumentNullException(nameof(birthDate));

            AddEvent(new PersonBasicInfoUpdated(
                    Id, Name.Value, Surname.Value, Gender, PersonalNumber.Value, BirthDate.Value, DateTimeOffset.UtcNow));
        }

        // Phones
        public PhoneNumber AddPhone(PhoneNumberType type, PhoneNumberNumber number)
        {
            var phone = PhoneNumber.Create(Id, type, number);
            _phoneNumbers.Add(phone);

            AddEvent(new PhoneAdded(Id, phone.Id, phone.Type, phone.Number.Value, DateTimeOffset.UtcNow));
            return phone;
        }

        public void UpdatePhone(Guid phoneId, PhoneNumberType? type = null, PhoneNumberNumber? number = null)
        {
            var phone = _phoneNumbers.FirstOrDefault(p => p.Id == phoneId)
                        ?? throw new DomainException($"Phone #{phoneId} not found.");
            if (type.HasValue) phone.ChangeType(type.Value);
            if (number is not null) phone.ChangeNumber(number);

            AddEvent(new PhoneUpdated(Id, phone.Id, phone.Type, phone.Number.Value, phone.Type, phone.Number.Value, DateTimeOffset.UtcNow));
        }

        public void RemovePhone(Guid phoneId)
        {
            var phone = _phoneNumbers.FirstOrDefault(p => p.Id == phoneId)
                        ?? throw new DomainException($"Phone #{phoneId} not found.");
            _phoneNumbers.Remove(phone);

            AddEvent(new PhoneRemoved(Id, phone.Id, DateTimeOffset.UtcNow));
        }

        public void ReplacePhones(IEnumerable<(PhoneNumberType Type, PhoneNumberNumber Number)> phones)
        {
            if (phones is null) throw new ArgumentNullException(nameof(phones));

            _phoneNumbers.Clear();
            foreach (var (type, number) in phones)
                AddPhone(type, number);

            AddEvent(new PhonesReplaced(Id, _phoneNumbers.Select(o => new PhoneSnapshot(o.Id,o.Type,o.Number)).ToList(), DateTimeOffset.UtcNow));
        }

        // Relations
        public PersonRelation AddRelation(Guid relatedPersonId, RelationType type)
        {
            if (relatedPersonId == Id)
                throw new DomainException("Cannot relate a person to themselves.");

            if (_outgoingRelations.Any(r => r.RelatedPersonId == relatedPersonId && r.Type == type))
                throw new DomainException("Relation already exists.");

            var rel = PersonRelation.Create(Id, relatedPersonId, type);
            _outgoingRelations.Add(rel);
            AddEvent(new PersonRelationAdded(Id, relatedPersonId, type, DateTimeOffset.UtcNow));

            return rel;
        }

        public void RemoveRelation(Guid relatedPersonId )
        {
            var rel = _outgoingRelations.FirstOrDefault(r => r.RelatedPersonId == relatedPersonId)
                      ?? throw new DomainException("Relation not found.");
            _outgoingRelations.Remove(rel);
            AddEvent(new PersonRelationRemoved(Id, relatedPersonId, DateTimeOffset.UtcNow));

        }

        

    }
}
