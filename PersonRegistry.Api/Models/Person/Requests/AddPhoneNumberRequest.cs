using PersonRegistry.Domain.Entities.Persons.Enums;

namespace PersonRegistry.Api.Models.Person.Requests
{
    public class AddPhoneNumberRequest
    {
        public Guid PersonId { get; set; }
        public PhoneNumberType Type { get; set; }
        public string Number { get; set; }
    }
}
