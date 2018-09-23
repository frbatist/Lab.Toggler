using Lab.Toggler.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab.Toggler.Domain.DTO
{
    public class FeatureDTO : DtoBase
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new FeatureDtoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
