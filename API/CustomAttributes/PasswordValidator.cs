using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using API.Utils;

namespace API.CustomAttributes {
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class PasswordAttribute: ValidationAttribute {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@!#$%&*_\-?{}()[\]:;',.+=])[a-zA-Z\d@!#$%&*_\-?{}()[\]:;',.+=]{16,}$"; // ? - ? ki tu, so, chu cai hoa thuong, ki tu dac biet
            if(value == null || !Regex.IsMatch((string) value, pattern)) {
                return new ValidationResult(ExceptionMessage.InvalidPassword);
            }
            return ValidationResult.Success;
        }
    }
}