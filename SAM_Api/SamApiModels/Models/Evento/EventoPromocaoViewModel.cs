using SamModelValidationRules.Attributes.Validation;
using System.ComponentModel.DataAnnotations;

namespace SamApiModels.Evento
{
    /// <summary>
    /// Representa os dados para promover um usuário
    /// </summary>
    public class EventoPromocaoViewModel
    {

        /// <summary>
        /// Indica se a promoção foi ou não aceita
        /// </summary>
        [Required]
        public bool FoiPromovido { get; set; }

        /// <summary>
        /// Identifica o evento de promoção o qual está sendo aprovado
        /// </summary>
        [Required]
        [ValidKey(ValidKeyAttribute.Entities.Evento)]
        public int Evento { get; set; }

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
        public EventoPromocaoViewModel()
        {

        }
    }
}
