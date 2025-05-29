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
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class ListMaxCountAttribute<T> : ValidationAttribute
    {
        public int MaxCount { get; private set; }
        public string DisplayName { get; private set; }

        public ListMaxCountAttribute(int maxCount, string displayName)
        {
            MaxCount = maxCount;
            DisplayName = displayName;
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture,
              ErrorMessageString, name);
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext.MemberName is null)
                return new ValidationResult("MemberName was null");

            var collectionProperty = validationContext.ObjectInstance.GetType().GetProperty(validationContext.MemberName);
            ICollection<T>? collection = (ICollection<T>?)collectionProperty?.GetValue(validationContext.ObjectInstance);

            if (collection is not null)
            {
                if (collection.Count > MaxCount)
                {
                    return new ValidationResult(FormatErrorMessage(DisplayName));
                }
            }

            return ValidationResult.Success!;
        }
    }
}
