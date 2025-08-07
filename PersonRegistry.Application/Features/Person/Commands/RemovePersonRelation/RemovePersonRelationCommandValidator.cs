using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Application.Features.Person.Commands.RemovePersonRelation
{
    public sealed class RemovePersonRelationCommandValidator : AbstractValidator<RemovePersonRelationCommand>
    {
        public RemovePersonRelationCommandValidator()
        {
            RuleFor(x => x.PersonId)
                .NotEmpty().WithMessage("PersonId must not be empty.");

            RuleFor(x => x.RelatedPersonId)
                .NotEmpty().WithMessage("RelatedPersonId must not be empty.");


        }
    }
}
