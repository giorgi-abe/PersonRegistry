using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Application.Features.Person.Commands.RemovePhoneNumber
{
    public class RemovePhoneNumberCommandValidator : AbstractValidator<RemovePhoneNumberCommand>
    {
        public RemovePhoneNumberCommandValidator()
        {
            RuleFor(x => x.PersonId).NotEmpty();
            RuleFor(x => x.PhoneNumberId).NotEmpty();
        }
    }
}
