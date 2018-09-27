using Lab.Toggler.Domain.Entities;
using Lab.Toggler.Domain.Validation;

namespace Lab.Toggler.Domain.DTO
{
    public class ApplicationFeatureDTO : DtoBase
    {
        public Feature Feature { get; set; }
        public Application Application { get; set; }
        public bool IsActive { get; protected set; }

        public ApplicationFeatureDTO(Feature feature, Application application, bool isActive)
        {
            Feature = feature;
            Application = application;
            IsActive = isActive;
        }

        public override bool IsValid()
        {
            ValidationResult = new ApplicationFeatureDtoValidation().Validate(this);
            return ValidationResult.IsValid;            
        }    
    }
}
