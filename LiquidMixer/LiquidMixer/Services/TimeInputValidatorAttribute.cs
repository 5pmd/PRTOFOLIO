using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidMixer.Services
{
    internal class TimeInputValidatorAttribute : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            var valueAsString = value as string;

            if (string.IsNullOrEmpty(valueAsString))
            {
                return new ValidationResult($"Time input is required.");
            }

            if (!int.TryParse((string?)value, out int volume))
            {
                return new ValidationResult($"Time input must be a number.");
            }
            if (volume < 0 || volume > 100)
            {
                return new ValidationResult($"Time input must be between 0 and 60.");
            }

            return ValidationResult.Success;
        }
    }
}
