using FluentValidation;
using Lab.Toggler.Domain.DTO;
using Lab.Toggler.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab.Toggler.Domain.Validation
{
    public class FeatureDtoValidation : AbstractValidator<FeatureDTO>
    {
        public FeatureDtoValidation()
        {
            RuleFor(d => d.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage(DomainMessageError.FeatureNameCannotBeNulllOrEmpty);
        }
    }
}
