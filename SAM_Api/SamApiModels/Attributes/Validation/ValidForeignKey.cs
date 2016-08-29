using DefaultException.Models;
using Opus.DataBaseEnvironment;
using System.Linq;
using SamDataBase.Model;
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
        private Type entityType;


        /// <summary>
        /// set the current repository
        /// </summary>
        /// <param name="repository"><!-- IRepository<T> where T is a class of our database model -->
        public ValidForeignKeyAttribute(Type entityType) 
        {
            this.entityType = entityType;
        }

        /// <summary>
        /// tell if the value is valid
        /// </summary>
        /// <param name="value">current value suplied to a field</param>
        /// <param name="validationContext">the validation context</param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int v = (int)value;
            object entity = null;
            if (entityType == typeof(Cargo))
            {
                using (var rep = DataAccess.Instance.GetCargoRepository())
                {
                    entity = rep.Find(c => c.id == v).SingleOrDefault();
                }
            }else if (entityType == typeof(Cargo))
            {

            }
          

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