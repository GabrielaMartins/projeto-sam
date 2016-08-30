using Opus.DataBaseEnvironment;
using System.Linq;
using SamDataBase.Model;
using System;
using System.ComponentModel.DataAnnotations;
using SamHelpers;
using System.Configuration;

namespace SamModelValidationRules.Attributes.Validation
{
    /// <summary>
    /// Allow values as FK
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ValidKeyAttribute : ValidationAttribute
    {
        /// <summary>
        /// Represents all entities where we can check the keys
        /// </summary>
        public enum Entities
        {
            /// <summary>
            /// Look int the table named 'Cargo'
            /// </summary>
            Cargo,

            /// <summary>
            /// Look int the table named 'Categoria'
            /// </summary>
            Categoria,

            /// <summary>
            /// Look int the table named 'Evento'
            /// </summary>
            Evento,

            /// <summary>
            /// Look int the table named 'Item'
            /// </summary>
            Item,

            /// <summary>
            /// Look int the table named 'Pendencia'
            /// </summary>
            Pendencia,

            /// <summary>
            /// Look int the table named 'Promocao'
            /// </summary>
            Promocao,

            /// <summary>
            /// Look int the table named 'Usuario'
            /// </summary>
            Usuario
        }

        private Entities entity;

        /// <summary>
        /// set a current table to check
        /// </summary>
        /// <param name="entity">
        /// is the entity type for check foreign key
        /// </param>
        public ValidKeyAttribute(Entities entity) 
        {
            this.entity = entity;
        }

   
        /// <summary>
        /// tell if the value is valid
        /// </summary>
        /// <param name="value">current value suplied to a field</param>
        /// <param name="validationContext">the validation context</param>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // if an entity is found, e is not null
            object e = null;

            #region Getting repositories
            if (entity == Entities.Cargo)
            {
                using (var rep = DataAccess.Instance.GetCargoRepository())
                {
                    int v = (int)value;
                    e = rep.Find(c => c.id == v).SingleOrDefault();
                }
            }
            else if (entity == Entities.Categoria)
            {
                using (var rep = DataAccess.Instance.GetCategoriaRepository())
                {
                    int v = (int)value;
                    e = rep.Find(c => c.id == v).SingleOrDefault();
                }
            }
            else if (entity == Entities.Evento)
            {
                using (var rep = DataAccess.Instance.GetEventoRepository())
                {
                    int v = (int)value;
                    e = rep.Find(c => c.id == v).SingleOrDefault();
                }
            }
            else if (entity == Entities.Item)
            {
                using (var rep = DataAccess.Instance.GetItemRepository())
                {
                    int v = (int)value;
                    e = rep.Find(c => c.id == v).SingleOrDefault();
                }
            }
            else if (entity == Entities.Pendencia)
            {
                using (var rep = DataAccess.Instance.GetPendenciaRepository())
                {
                    int v = (int)value;
                    e = rep.Find(c => c.id == v).SingleOrDefault();
                }
            }
            else if (entity == Entities.Promocao)
            {
                using (var rep = DataAccess.Instance.GetPromocaoRepository())
                {
                    int v = (int)value;
                    e = rep.Find(c => c.id == v).SingleOrDefault();
                }
            }
            else if (entity == Entities.Usuario)
            {
                using (var rep = DataAccess.Instance.GetUsuarioRepository())
                {
                    if (value is int)
                    {
                        var val = (int)value;
                        e = rep.Find(c => c.id == val).SingleOrDefault();
                    }
                    else if (value is string)
                    {
                        var val = value as string;
                        ActiveDirectoryHelper adConsumer = new ActiveDirectoryHelper(ConfigurationManager.AppSettings["OpusADServer"]);
                        e = adConsumer.GetUser(val);
                    }
                }
            }
            //else if (entity == Entities.ItensTagged)
            //{
            //    using (var rep = DataAccess.Instance.GetItensTaggedRepository())
            //    {
            //        entity = rep.Find(c => c.id == v).SingleOrDefault();
            //    }
            //}
            //else if (entity == Entities.Tag)
            //{
            //    using (var rep = DataAccess.Instance.GetTagRepository())
            //    {
            //        entity = rep.Find(c => c.id == v).SingleOrDefault();
            //    }
            //}
            //else if (entity == Entities.ResultadoVotacao)
            //{
            //    using (var rep = DataAccess.Instance.GetResultadoVotacaoRepository())
            //    {
            //        entity = rep.Find(c => c.id == v).SingleOrDefault();
            //    }
            //}
            #endregion

            // if an error
            if (e == null)
            {
             
                // The string before '|' is the title.
                // The string after '|' is the detail.
                return new ValidationResult("Invalid value supplied.|" +
                                            $"'{value}' is invalid value to '{validationContext.ObjectType}.{validationContext.MemberName}'. " +
                                            $"Check if it's a valid key");
            }

            return null;
        }

    }
}