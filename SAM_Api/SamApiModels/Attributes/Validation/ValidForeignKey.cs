using DefaultException.Models;
using Opus.RepositoryPattern;
using System;
using System.ComponentModel.DataAnnotations;

namespace SamModelValidationRules.Attributes.Validation
{
    /// <summary>
    /// Allow values in array
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ValidForeignKeyAttribute : ValidationAttribute
    {
        private IRepository<dynamic> repository;


        /// <summary>
        /// set the current repository
        /// </summary>
        /// <param name="repository"><!-- IRepository<T> where T is a class of our database model -->
        public ValidForeignKeyAttribute(object repository) 
        {
            this.repository = repository as IRepository<dynamic>;
        }

        /// <summary>
        /// tell if the value is valid
        /// </summary>
        /// <param name="value">current value suplied to a field</param>
        /// <param name="validationContext">the validation context</param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var entity = repository.Find((int)value);

            // if an error
            if (entity == null)
            {
             
                // The string before '|' is the title.
                // The string after '|' is the detail.
                return new ValidationResult("Invalid value supplied.|" +
                                            $"Invalid value supplied to '{validationContext.ObjectType}.{validationContext.MemberName}'. " +
                                            $"Valid values: 'a valid FK key'");
            }

            return null;
        }
    }
}