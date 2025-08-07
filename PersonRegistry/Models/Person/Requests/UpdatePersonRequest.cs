using PersonRegistry.Domain.Entities.Persons.Enums;

namespace PersonRegistry.Api.Models.Person.Requests
{

    public sealed class UpdatePersonRequest
    {
        public Guid PersonId { get; set; }
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public GenderType Gender { get; set; }
        public string PersonalNumber { get; set; } = default!;
        public DateOnly BirthDate { get; set; }
    }
}
