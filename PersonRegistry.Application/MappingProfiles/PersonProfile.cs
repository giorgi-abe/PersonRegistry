using AutoMapper;
using PersonRegistry.Application.Features.Person.Commands.AddPerson;
using PersonRegistry.Application.Features.Person.Commands.AddPersonRelation;
using PersonRegistry.Application.Features.Person.Commands.UpdatePersonBasicInfo;
using PersonRegistry.Application.Features.Person.Queries.GetPersonById;
using PersonRegistry.Application.Features.Person.Queries.SearchPeople;
using PersonRegistry.Domain.Entities.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Application.MappingProfiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            // Mapping for GetPersonById
            CreateMap<Person, GetPersonByIdModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Value))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname.Value))
                .ForMember(dest => dest.PersonalNumber, opt => opt.MapFrom(src => src.PersonalNumber.Value))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.Value))
                .ForMember(dest => dest.Phones, opt => opt.MapFrom(src => src.PhoneNumbers));

            CreateMap<PhoneNumber, GetPersonByIdPhoneModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number.Value));
            
            CreateMap<PersonRelation, GetPersonByIdPersonRelationModel>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.RelatedPersonId, opt => opt.MapFrom(src => src.RelatedPersonId));


            // Mapping for PersonListItemDto (Search)
            CreateMap<Person, SearchPeopleModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Value))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname.Value))
                .ForMember(dest => dest.PersonalNumber, opt => opt.MapFrom(src => src.PersonalNumber.Value))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.Value))
                .ForMember(dest => dest.Phones, opt => opt.MapFrom(src => src.PhoneNumbers));

            // Mapping for PhoneNumber → PhoneDto
            CreateMap<PhoneNumber, SearchPeoplePhoneModel>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number.Value));
            
            
        }
    }
}
