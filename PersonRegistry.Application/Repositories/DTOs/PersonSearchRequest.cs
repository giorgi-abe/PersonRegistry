using PersonRegistry.Common.Contracts;
using PersonRegistry.Domain.Entities.Persons.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Application.Repositories.DTOs
{
    public class PersonSearchRequest : IPagingRequest
    {
        private const int DefaultPage = 1;
        private const int DefaultPageSize = 20;

        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? PersonalNumber { get; set; }
        public GenderType? Gender { get; set; }

        private int? _page;
        public int Page
        {
            get => _page ?? DefaultPage;
            set => _page = value > 0 ? value : DefaultPage;
        }

        private int? _pageSize;
        public int PageSize
        {
            get => _pageSize ?? DefaultPageSize;
            set => _pageSize = value > 0 ? value : DefaultPageSize;
        }
    }
}
