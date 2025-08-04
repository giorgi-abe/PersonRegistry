using PersonRegistry.Common.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PersonRegistry.Domain.Entities.Persons.ValueObjects
{
    public sealed record PersonName
    {
        public string Value { get; }

        public PersonName(string value)
        {

            ArgumentException.ThrowIfNullOrWhiteSpace(value);

            if (value.Length is < 2 or > 50) throw new DomainException("Name length 2-50");
            if (!(CommonRegex.GeorgianLetters.IsMatch(value) ^ CommonRegex.GeorgianLetters.IsMatch(value))) 
                throw new DomainException("Use either Georgian OR Latin letters only");

            Value = value.Trim();
        }


        public static implicit operator string(PersonName name)
            => name.Value;

        public static implicit operator PersonName(string name)
            => new(name);
    }
}
