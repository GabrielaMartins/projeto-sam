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

        private Type type;

        /// <summary>
        /// set the current repository
        /// </summary>
        /// <param name="repository"><!-- IRepository<T> where T is a class of our database model -->
        public ValidForeignKeyAttribute(Type entityType) 
        {
            type = entityType;
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
            if (type == typeof(Cargo))
            {
                using (var rep = DataAccess.Instance.GetCargoRepository())
                {
                    entity = rep.Find(c => c.id == v).SingleOrDefault();
                }
            }
            else if (type == typeof(Categoria))
            {
                using (var rep = DataAccess.Instance.GetCategoriaRepository())
                {
                    entity = rep.Find(c => c.id == v).SingleOrDefault();
                }
            }
            else if (type == typeof(Evento))
            {
                using (var rep = DataAccess.Instance.GetEventoRepository())
                {
                    entity = rep.Find(c => c.id == v).SingleOrDefault();
                }
            }
            else if (type == typeof(Item))
            {
                using (var rep = DataAccess.Instance.GetItemRepository())
                {
                    entity = rep.Find(c => c.id == v).SingleOrDefault();
                }
            }
            else if (type == typeof(Pendencia))
            {
                using (var rep = DataAccess.Instance.GetPendenciaRepository())
                {
                    entity = rep.Find(c => c.id == v).SingleOrDefault();
                }
            }
            else if (type == typeof(Promocao))
            {
                using (var rep = DataAccess.Instance.GetPromocaoRepository())
                {
                    entity = rep.Find(c => c.id == v).SingleOrDefault();
                }
            }
            else if (type == typeof(Usuario))
            {
                using (var rep = DataAccess.Instance.GetUsuarioRepository())
                {
                    entity = rep.Find(c => c.id == v).SingleOrDefault();
                }
            }
            //else if (type == typeof(ItensTagged))
            //{
            //    using (var rep = DataAccess.Instance.GetItensTaggedRepository())
            //    {
            //        entity = rep.Find(c => c.id == v).SingleOrDefault();
            //    }
            //}
            //else if (type == typeof(Tag))
            //{
            //    using (var rep = DataAccess.Instance.GetTagRepository())
            //    {
            //        entity = rep.Find(c => c.id == v).SingleOrDefault();
            //    }
            //}
            //else if (type == typeof(ResultadoVotacao))
            //{
            //    using (var rep = DataAccess.Instance.GetResultadoVotacaoRepository())
            //    {
            //        entity = rep.Find(c => c.id == v).SingleOrDefault();
            //    }
            //}

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