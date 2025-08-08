using AutoMapper;
using PersonRegistry.Common.Domains;
using PersonRegistry.Domain.Entities.Persons;
using PersonRegistry.Domain.Entities.Persons.Enums;
using PersonRegistry.Infrastructure.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Infrastructure.Persistence.MappingProfiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            // Domain -> Entity mappings
            CreateMap<Person, PersonEntity>()
                .ForMember(d => d.BirthDate, o => o.MapFrom(s => s.BirthDate.Value))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name.Value))
                .ForMember(d => d.Surname, o => o.MapFrom(s => s.Surname.Value))
                .ForMember(d => d.PersonalNumber, o => o.MapFrom(s => s.PersonalNumber.Value)); 

            CreateMap<PersonRelation, PersonRelationEntity>();
            CreateMap<PhoneNumber, PhoneNumberEntity>();

            CreateMap<PersonEntity, Person>()
                .ForMember(d => d.Gender, o => o.MapFrom(s => Enum.Parse<GenderType>(s.Gender, true)))
                .ForMember(d => d.PhoneNumbers, o => o.MapFrom(s=> s.PhoneNumbers))
                //.ForPath(d => d.Surname.Value, o => o.MapFrom(s => s.Surname))
                //.ForPath(d => d.Name.Value, o => o.MapFrom(s => s.Name))
                //.ForPath(d => d.PersonalNumber.Value, o => o.MapFrom(s => s.PersonalNumber))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<PersonRelationEntity, PersonRelation>()
                .ForMember(d => d.Type, o => o.MapFrom(s => Enum.Parse<RelationType>(s.Type, true)));
            CreateMap<PhoneNumberEntity, PhoneNumber>()
                .ForMember(d => d.Type, o => o.MapFrom(s => Enum.Parse<PhoneNumberType>(s.Type, true)));

        }
    }
}


