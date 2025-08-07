using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Application.Features.Person.Commands.AddPersonRelation
{
    

    public class AddPersonRelationCommandValidator : AbstractValidator<AddPersonRelationCommand>
    {
        public AddPersonRelationCommandValidator()
        {
            RuleFor(x => x.PersonId)
                .NotEmpty().WithMessage("PersonId is required.");

            RuleFor(x => x.RelatedPersonId)
                .NotEmpty().WithMessage("RelatedPersonId is required.");

            RuleFor(x => x.RelationType)
                .IsInEnum().WithMessage("Invalid RelationType.");
        }
    }
}
