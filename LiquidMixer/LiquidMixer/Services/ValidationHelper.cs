using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidMixer.Services
{
    internal class ValidationHelper
    {

        public static void ValidateProperty(object target, object? propertyValue, string? propertyName, Dictionary<string, List<string?>> result)
        {

            if (propertyValue is null || propertyName is null) return;

            var validationContext = new ValidationContext(target) { MemberName = propertyName };
            var validationResult = new List<ValidationResult>();

            Validator.TryValidateProperty(propertyValue, validationContext, validationResult);


            result.Remove(propertyName);

            if (validationResult.Count > 0)
            {
                result[propertyName] = validationResult.Select(x => x.ErrorMessage).ToList();
            }




        }
    }
}
