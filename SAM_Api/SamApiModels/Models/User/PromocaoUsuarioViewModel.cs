using SamModelValidationRules.Attributes.Validation;
using System.ComponentModel.DataAnnotations;

namespace SamApiModels.User
{
    /// <summary>
    /// Representa os dados para promover um usuário
    /// </summary>
    public class PromocaoUsuarioViewModel
    {
        /// <summary>
        /// Identifica o usuário que receberá a promoção
        /// </summary>
        [Required]
        [ValidKey(ValidKeyAttribute.Entities.Usuario)]
        public string Usuario { get; set; }

        /// <summary>
        /// Representa o cargo para o qual o usuário será promovido
        /// </summary>
        [Required]
        [ValidKey(ValidKeyAttribute.Entities.Cargo)]
        public int Cargo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public PromocaoUsuarioViewModel()
        {

        }
    }
}
