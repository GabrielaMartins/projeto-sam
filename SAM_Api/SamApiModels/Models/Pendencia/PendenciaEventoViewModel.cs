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
        [ValidKey(ValidKeyAttribute.Entities.Evento)]
        public int id { get; set; }

        /// <summary>
        /// Identifica se o evento foi ou não encerrado
        /// </summary>
        [Required]
        public bool estado { get; set; }
        
        /// <summary>
        /// Identifica o tipo de evento
        /// </summary>
        [Required]
        [AllowedValues(new[] {"votacao","atribuicao","promocao","agendamento"})]
        public string tipo { get; set; }

        /// <summary>
        /// Identifica a data que o evento ocorreu
        /// </summary>
        [Required]
        [ValidDate]
        public DateTime data { get; set; }

        /// <summary>
        /// Identifica o item relacionado ao evento
        /// </summary>
        [Required]
        public ItemViewModel Item { get; set; }


        /// <summary>
        /// ESTA DANDO ERRO NO MAPPING
        /// Identifica o evento cujo a pendência foi gerada
        /// </summary>
        //[Required]
        //public UsuarioViewModel Usuario { get; set; }


        /// <summary>
        /// Construtor do objeto
        /// </summary>
        public PendenciaEventoViewModel()
        {

        }
    }
}
