using Lab.Toggler.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab.Toggler.Domain.DTO
{
    public class FeatureDTO : DtoBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public FeatureDTO(string name, bool isActive)
        {
            Name = name;
            IsActive = isActive;
        }

        public FeatureDTO(int id, string name, bool isActive)
        {
            Id = id;
            Name = name;
            IsActive = isActive;
        }

        public override bool IsValid()
        {
            ValidationResult = new FeatureDtoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
