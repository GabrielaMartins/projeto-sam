using DefaultException.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace SamModelValidationRules.Attributes.Validation
{
    /// <summary>
    /// Allow values in array
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class AllowedValuesAttribute : ValidationAttribute
    {
        private object[] values;

        /// <summary>
        /// set the current valid values
        /// </summary>
        /// <param name="values">array of valid values for a field</param>
        public AllowedValuesAttribute(object[] values)
        {
            this.values = values;
        }

        /// <summary>
        /// tell if the value is valid
        /// </summary>
        /// <param name="value">current value suplied to a field</param>
        /// <param name="validationContext">the validation context</param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
                   
            // if an error
            if (!Array.Exists(values, x => x.Equals(value)))
            {
                string validValues = string.Join(",", values);
               
                // The string before '|' is the title.
                // The string after '|' is the detail.
                return new ValidationResult("Invalid value supplied.|" +
                                            $"'{value}' is invalid value to '{validationContext.ObjectType}.{validationContext.MemberName}'. " +
                                            $"Valid values: '({validValues})'");
            }

            return null;
        }
    }
}