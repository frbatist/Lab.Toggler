using FluentValidation;
using Lab.Toggler.Domain.DTO;
using Lab.Toggler.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab.Toggler.Domain.Validation
{
    public class ApplicationDtoValidation : AbstractValidator<ApplicationDTO>
    {
        public ApplicationDtoValidation()
        {
            RuleFor(d => d.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage(DomainMessageError.ApplicationNameCannotBeNullOrEmpty);

            RuleFor(d => d.Version)
                .NotNull()
                .NotEmpty()
                .WithMessage(DomainMessageError.ApplicationVersionCannotBeNullOrEmpty);
        }
    }
}
