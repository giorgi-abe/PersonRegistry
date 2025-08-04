using PersonRegistry.Common.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PersonRegistry.Domain.Entities.Persons.ValueObjects
{
    public sealed record PersonSurname
    {
        public string Value { get; }

        public PersonSurname(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new NameInvalidException("Surname is required.");

            value = value.Trim();
            if (value.Length is < 2 or > 50)
                throw new NameInvalidException("Surname length must be 2–50.");

            if (!(CommonRegex.GeorgianLetters.IsMatch(value) ^ CommonRegex.GeorgianLetters.IsMatch(value)))
                throw new NameInvalidException("Use either Georgian OR Latin letters only.");

            Value = value;
        }

        public static implicit operator string(PersonSurname surname) => surname.Value;
        public static implicit operator PersonSurname(string value) => new(value);
    }
}
