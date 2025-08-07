using PersonRegistry.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Domain.Entities.Persons.ValueObjects
{
    public sealed record PersonBirthDate
    {
        public DateOnly Value { get; }
        public const int MinAgeYears = 18;

        public PersonBirthDate(DateOnly value, DateOnly? today = null)
        {
            var now = today ?? DateOnly.FromDateTime(DateTime.UtcNow);
            if (value > now) throw new DomainException("Birth date cannot be in the future.");

            var minBirth = new DateOnly(now.Year - MinAgeYears, now.Month, now.Day);
            if (value > minBirth) throw new DomainException($"Person must be at least {MinAgeYears} years old.");
            Value = value;
        }

        public static implicit operator DateOnly(PersonBirthDate b) => b.Value;
        public static implicit operator PersonBirthDate(DateOnly d) => new(d);
        public override string ToString() => Value.ToString("yyyy-MM-dd");
    }
}
