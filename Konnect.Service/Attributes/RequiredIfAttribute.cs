using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Konnect.Service.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class RequiredIfAttribute : ValidationAttribute
    {
        #region Properties

        public string OtherProperty { get; private set; } = string.Empty;
        public string? OtherPropertyDisplayName { get; set; }
        public object? OtherPropertyValue { get; private set; }
        public bool IsInverted { get; set; }
        public override bool RequiresValidationContext
        {
            get { return true; }
        }

        #endregion

        #region Constructor

        public RequiredIfAttribute(string otherProperty, object otherPropertyValue)
            : base("'{0}' is required because '{1}' has a value {3}'{2}'.")
        {
            OtherProperty = otherProperty;
            OtherPropertyValue = otherPropertyValue;
            IsInverted = false;
        }

        #endregion

        public override string FormatErrorMessage(string name)
        {
            return string.Format(
                CultureInfo.CurrentCulture,
                ErrorMessageString,
                name,
                OtherPropertyDisplayName ?? OtherProperty,
                OtherPropertyValue,
                IsInverted ? "other than " : "of ");
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext == null)
            {
                throw new ArgumentNullException("validationContext");
            }

            PropertyInfo? otherProperty = validationContext.ObjectType.GetProperty(OtherProperty);
            if (otherProperty == null)
            {
                return new ValidationResult(
                    string.Format(CultureInfo.CurrentCulture, "Could not find a property named '{0}'.", OtherProperty));
            }

            object? otherValue = otherProperty.GetValue(validationContext.ObjectInstance);

            if (!IsInverted && Equals(otherValue, OtherPropertyValue) ||
                IsInverted && !Equals(otherValue, OtherPropertyValue))
            {
                if (value == null)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }

                string? val = value as string;
                if (val != null && val.Trim().Length == 0)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return ValidationResult.Success!;
        }
    }
}
