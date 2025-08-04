using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PersonRegistry.Common.Validation
{
    public static class CommonRegex
    {
        public static readonly Regex GeorgianLetters =
            new(@"^[\p{IsGeorgian}]{2,50}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public static readonly Regex LatinLetters =
            new(@"^[A-Za-z]{2,50}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public static readonly Regex PhoneAllowed =
            new(@"^[0-9+\-()\s]{4,50}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public static readonly Regex ElevenDigits =
            new(@"^[0-9]{11}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);
    }
}
