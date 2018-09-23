using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab.Toggler.Domain.DTO
{
    public abstract class DtoBase
    {
        public ValidationResult ValidationResult { get; set; }
        public abstract bool IsValid();
    }
}
