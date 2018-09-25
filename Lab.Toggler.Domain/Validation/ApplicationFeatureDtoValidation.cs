using FluentValidation;
using Lab.Toggler.Domain.DTO;
using Lab.Toggler.Domain.Resources;

namespace Lab.Toggler.Domain.Validation
{
    public class ApplicationFeatureDtoValidation : AbstractValidator<ApplicationFeatureDTO>
    {
        public ApplicationFeatureDtoValidation()
        {
            RuleFor(d => d.Application)
                .NotNull()
                .WithMessage(DomainMessageError.ApplicationCannotBeNull);

            RuleFor(d => d.Feature)
                .NotNull()
                .WithMessage(DomainMessageError.FeatureCannotBeNull);
        }
    }
}
