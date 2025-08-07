namespace PersonRegistry.Api.Models.Person.Requests
{
    public class SearchPeopleRequestModel
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? PersonalNumber { get; set; }
        public string? Gender { get; set; }
        public DateOnly? BirthDate { get; set; }
        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 20;
    }
}
