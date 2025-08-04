using PersonRegistry.Common.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PersonRegistry.Domain.Entities.Persons.ValueObjects
{
    public sealed record PhoneNumberNumber
    {
        public string Value { get; }

        public PhoneNumberNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new PhoneNumberInvalidException("Number is required.");

            var v = value.Trim();

            // Allowed characters and length
            if (v.Length is < 4 or > 50)
                throw new PhoneNumberInvalidException("Length must be 4–50.");

            if (!CommonRegex.PhoneAllowed.IsMatch(value))
                throw new PhoneNumberInvalidException("Allowed: digits, spaces, +, -, ( ).");

            // Optional light normalization (collapse internal multiple spaces)
            v = Regex.Replace(v, @"\s{2,}", " ");

            Value = v;
        }

        public override string ToString() => Value;

        public static implicit operator string(PhoneNumberNumber n) => n.Value;
        public static implicit operator PhoneNumberNumber(string n) => new(n);
    }
}
