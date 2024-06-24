using System.ComponentModel.DataAnnotations;
using API.Utils;

namespace API.CustomAttributes {
    [AttributeUsage(AttributeTargets.Class)]
    public class EqualAttribute: ValidationAttribute {
        private string _propertyName1;
        private string _propertyName2;

        public EqualAttribute(string propertyName1, string propertyName2)
        {
            _propertyName1 = propertyName1;
            _propertyName2 = propertyName2;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var value1 = validationContext.ObjectInstance.GetType().GetProperty(this._propertyName1)?.GetValue(validationContext.ObjectInstance);
            var value2 = validationContext.ObjectInstance.GetType().GetProperty(this._propertyName2)?.GetValue(validationContext.ObjectInstance);
            if (value1 == null ||
                value2 == null ||
                !((string) value1).Equals((string) value2)) {
                return new ValidationResult($"{_propertyName2} is not equal to {_propertyName1}");
            }
            return ValidationResult.Success;
        }
    }
}