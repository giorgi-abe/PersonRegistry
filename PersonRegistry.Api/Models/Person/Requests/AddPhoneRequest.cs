using PersonRegistry.Domain.Entities.Persons.Enums;

namespace PersonRegistry.Api.Models.Person.Requests
{

    public class AddPhoneRequest
    {
        public PhoneNumberType Type { get; set; }
        public string Number { get; set; }
    }

}
