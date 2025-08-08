using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Application.Features.Person.Commands.AddPhoneNumber
{
    public class AddPhoneNumberCommandValidator : AbstractValidator<AddPhoneNumberCommand>
    {
        public AddPhoneNumberCommandValidator()
        {
            RuleFor(x => x.PersonId).NotEmpty();
            RuleFor(x => x.Number).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Type).IsInEnum();
        }
    }
}
