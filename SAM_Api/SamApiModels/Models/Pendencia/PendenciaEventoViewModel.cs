using SamApiModels.Item;
using SamApiModels.User;
using SamModelValidationRules.Attributes.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace SamApiModels.Pendencia
{
    /// <summary>
    /// Representa o evento associado a uma pendência do SAM
    /// </summary>
    public class PendenciaEventoViewModel
    {
        /// <summary>
        /// Identifica o evento cujo a pendência foi gerada
        /// </summary>
        [Required]
        [ValidKey(ValidKeyAttribute.Entities.Evento, ErrorMessage = "Invalid value supplied to PendenciaEventoViewModel. Check if it's a valid key")]
        public int id { get; set; }

        /// <summary>
        /// Identifica se o evento foi ou não aceito
        /// </summary>
        [Required]
        public bool estado { get; set; }

        /// <summary>
        /// Identifica se o evento foi ou não processado
        /// </summary>
        [Required]
        public bool processado { get; set; }

        /// <summary>
        /// Identifica o tipo de evento
        /// </summary>
        [Required]
        [AllowedValues(new object[] {"votacao","atribuicao","promocao","agendamento"})]
        public string tipo { get; set; }

        /// <summary>
        /// Identifica a data que o evento ocorreu
        /// </summary>
        [Required]
        [RegularExpression(@"^\d{2}/\d{2}/\d{4}$", ErrorMessage = "Invalid date format")]
        public DateTime data { get; set; }

        /// <summary>
        /// Identifica o item relacionado ao evento
        /// </summary>
        [Required]
        public ItemViewModel Item { get; set; }


        /// <summary>
        /// Representa o usuário que fez o evento
        /// </summary>
        [Required]
        public UsuarioViewModel Usuario { get; set; }


        /// <summary>
        /// Construtor do objeto
        /// </summary>
        public PendenciaEventoViewModel()
        {

        }
    }
}
