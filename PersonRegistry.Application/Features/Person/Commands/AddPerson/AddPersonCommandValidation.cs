using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Application.Features.Person.Commands.AddPerson
{
    public class AddPersonCommandValidation : AbstractValidator<AddPersonCommand>
    {
        public AddPersonCommandValidation()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name is required.")
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(x => x.Surname)
                .NotNull().WithMessage("Surname is required.")
                .NotEmpty().WithMessage("Surname is required.");

            RuleFor(x => x.PersonalNumber)
                .NotNull().WithMessage("Personal number is required.")
                .NotEmpty().WithMessage("Personal number is required.");

            RuleFor(x => x.BirthDate)
                .NotNull().WithMessage("Birth date is required.");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Gender must be a valid value.");
        }
    }
}
