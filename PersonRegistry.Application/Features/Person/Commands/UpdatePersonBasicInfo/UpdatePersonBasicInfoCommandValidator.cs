using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Application.Features.Person.Commands.UpdatePersonBasicInfo
{
    internal sealed class UpdatePersonBasicInfoCommandValidator : AbstractValidator<UpdatePersonBasicInfoCommand>
    {
        public UpdatePersonBasicInfoCommandValidator()
        {
            RuleFor(x => x.PersonId)
                .NotEmpty().WithMessage("PersonId is required.");

            RuleFor(x => x.Name)
                .NotEmpty().MinimumLength(2).MaximumLength(50);

            RuleFor(x => x.Surname)
                .NotEmpty().MinimumLength(2).MaximumLength(50);

            RuleFor(x => x.PersonalNumber)
                .NotEmpty().Length(11);

            RuleFor(x => x.BirthDate)
                .NotEmpty().LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));

            RuleFor(x => x.Gender)
                .IsInEnum();
        }
    }
}
