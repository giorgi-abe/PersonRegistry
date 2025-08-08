using AutoMapper;
using PersonRegistry.Api.Models.Person.Requests;
using PersonRegistry.Application.Features.Person.Commands.AddPerson;
using PersonRegistry.Application.Features.Person.Commands.AddPersonRelation;
using PersonRegistry.Application.Features.Person.Commands.AddPhoneNumber;
using PersonRegistry.Application.Features.Person.Commands.UpdatePersonBasicInfo;
using PersonRegistry.Application.Features.Person.Queries.SearchPeople;
using PersonRegistry.Domain.Entities.Persons.Enums;

namespace PersonRegistry.Api.MappingProfiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<AddPersonRequest, AddPersonCommand>();

            CreateMap<UpdatePersonRequest, UpdatePersonBasicInfoCommand>();


            CreateMap<AddPersonRelationRequest, AddPersonRelationCommand>();
            CreateMap<AddPhoneRequest, PhoneInput>();
            CreateMap<AddPersonRequest, AddPersonCommand>()
                .ConstructUsing(src => new AddPersonCommand(
                    src.Name,
                    src.Surname,
                    src.Gender,
                    src.PersonalNumber,
                    src.BirthDate,
                    src.Phones.Select(p => new PhoneInput(p.Type, p.Number)).ToList()
                ));
            CreateMap<AddPhoneNumberRequest, AddPhoneNumberCommand>();
            CreateMap<SearchPeopleRequestModel, SearchPeopleQuery>();
        }
    }
}
