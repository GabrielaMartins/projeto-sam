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
        private object values;

        /// <summary>
        /// set the current valid values
        /// </summary>
        /// <param name="values">array of valid values for a field</param>
        public AllowedValuesAttribute(object values)
        {
<<<<<<< HEAD
=======
            if(!(values is Array))
            {
                throw new Exception("You must pass an array as values");
            }

>>>>>>> master
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
            var array = values as object[];
            
            // if an error
            if (!Array.Exists(array, x => x.Equals(value)))
            {
                string validValues = string.Join(",", array);
               
                // The string before '|' is the title.
                // The string after '|' is the detail.
                return new ValidationResult("Invalid value supplied.|" +
                                            $"Invalid value supplied to '{validationContext.ObjectType}.{validationContext.MemberName}'. " +
                                            $"Valid values: '({validValues})'");
            }

            return null;
        }
    }
}