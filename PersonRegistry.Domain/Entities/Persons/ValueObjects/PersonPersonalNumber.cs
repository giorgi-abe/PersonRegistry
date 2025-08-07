using PersonRegistry.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PersonRegistry.Domain.Entities.Persons.ValueObjects
{
    public sealed record PersonPersonalNumber
    {
        public string Value { get; }

        public PersonPersonalNumber(string value)
        {
            value ??= string.Empty;
            if (!Regex.IsMatch(value, @"^\d{11}$"))
                throw new DomainException("Personal number must be exactly 11 digits.");

            Value = value;
        }

        public static implicit operator string(PersonPersonalNumber pn) => pn.Value;
        public static implicit operator PersonPersonalNumber(string value) => new(value);
    }
}
