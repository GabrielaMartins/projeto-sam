using System;
using System.ComponentModel.DataAnnotations;

namespace SamModelValidationRules.Attributes.Validation
{
    /// <summary>
    /// Allow values in array
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ValidPictureAttribute : ValidationAttribute
    {
        private string error;

        /// <summary>
        /// Constructor
        /// </summary>
        public ValidPictureAttribute()
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

            // find this pattern
            //data:image/jpeg;base64,
            var str = value as string;
            if (!System.Text.RegularExpressions.Regex.IsMatch(str, @"data:image\/(.*),"))
            {
                ErrorMessage = "Invalid value supplied.|" +
                                $"Invalid value supplied to '{validationContext.ObjectType}.{validationContext.MemberName}'. " +
                                "Valid values: a valid string in the format 'data:image/jpeg;base64,{bytes}'";

                return new ValidationResult(ErrorMessage);
            }

            try
            {
                var s = str.Split(',');
                Convert.FromBase64String(s[1]);
            }
            catch
            {
                ErrorMessage = "Invalid value supplied.|" +
                               $"Invalid value supplied to '{validationContext.ObjectType}.{validationContext.MemberName}'. " +
                               $"Valid values: 'a valid string base64 encoded'";

                return new ValidationResult(ErrorMessage);
            }

            return null;
        }
    }
}