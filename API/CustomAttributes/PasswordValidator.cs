using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using API.Utils;

namespace API.CustomAttributes {
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class PasswordAttribute: ValidationAttribute {
        private const string PasswordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@!#$%&*_\-?{}()[\]:;',.+=])[a-zA-Z\d@!#$%&*_\-?{}()[\]:;',.+=]{16,}$";
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value == null || !Regex.IsMatch((string) value, PasswordPattern)) {
                return new ValidationResult(ExceptionMessage.InvalidPassword);
            }
            return ValidationResult.Success;
        }
    }
}