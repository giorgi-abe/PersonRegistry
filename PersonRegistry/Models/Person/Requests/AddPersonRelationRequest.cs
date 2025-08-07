using PersonRegistry.Domain.Entities.Persons.Enums;

namespace PersonRegistry.Api.Models.Person.Requests
{
    public sealed class AddPersonRelationRequest
    {
        public Guid PersonId { get; set; }
        public Guid RelatedPersonId { get; set; }
        public RelationType RelationType { get; set; }
    }
}
