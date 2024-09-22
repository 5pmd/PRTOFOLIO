using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidMixer.Services
{
    internal class LiquidInputValidatorAttribute : ValidationAttribute
    {

        private readonly string _propertyName;
        public LiquidInputValidatorAttribute(string propertyName)
        {
            _propertyName = propertyName;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            var valueAsString = value as string;

            if (string.IsNullOrEmpty(valueAsString))
            {
                return new ValidationResult($"Input Volume is required at {_propertyName}.");
            }

            if (!int.TryParse((string?)value, out int volume))
            {
                return new ValidationResult($"Volume must be a number at {_propertyName}.");
            }
            if (volume < 0 || volume > 100)
            {
                return new ValidationResult($"Volume must be between 0 and 100 at {_propertyName}.");
            }

            return ValidationResult.Success;
        }
    }
}
