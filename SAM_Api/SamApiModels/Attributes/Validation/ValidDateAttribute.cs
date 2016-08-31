using DefaultException.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace SamModelValidationRules.Attributes.Validation
{
    /// <summary>
    /// Allow values in array
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ValidDateAttribute : ValidationAttribute
    {
 
        /// <summary>
        /// Construtor do objeto
        /// </summary>
        public ValidDateAttribute()
        {
           
        }

        /// <summary>
        /// tell if the value is valid
        /// </summary>
        /// <param name="value">current value suplied to a field</param>
        /// <param name="validationContext">the validation context</param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //verify if date's format is, for instance, 10-10-2010
            // if an error
            var str = value as string;
            if (!System.Text.RegularExpressions.Regex.IsMatch(str, @"^\d{2}-\d{2}-\d{4}$"))
            {
               
                // The string before '|' is the title.
                // The string after '|' is the detail.
                return new ValidationResult("Invalid value supplied.|" +
                                            $"Invalid value to date, please, write a date with value as the example: 10-10-2010");
            }

            return null;
        }
    }
}