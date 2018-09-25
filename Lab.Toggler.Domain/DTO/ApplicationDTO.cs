using Lab.Toggler.Domain.Validation;

namespace Lab.Toggler.Domain.DTO
{
    public class ApplicationDTO : DtoBase
    {
        public string Name { get; set; }
        public string Version { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new ApplicationDtoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
