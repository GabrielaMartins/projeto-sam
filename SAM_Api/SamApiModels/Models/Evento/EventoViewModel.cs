using SamApiModels.User;
using SamModelValidationRules.Attributes.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace SamApiModels.Evento
{

    /// <summary>
    /// Representa um evento do SAM
    /// </summary>
    public class EventoViewModel
    {
        /// <summary>
        /// Identifica o evento do SAM
        /// </summary>
        [Required]
        [ValidKey(ValidKeyAttribute.Entities.Evento)]
        public int id { get; set; }

        /// <summary>
        /// Representa se o evento foi ou não aceito
        /// </summary>
        [Required]
        public bool estado { get; set; }

        /// <summary>
        /// Representa se o evento foi ou não encerrado
        /// </summary>
        [Required]
        public bool processado { get; set; }

        /// <summary>
        /// Representa o tipo do evento
        /// </summary>
        [Required]
        [AllowedValues(new object[] {"votacao", "atribuicao", "promocao", "agendamento" })]
        public string tipo { get; set; }

        /// <summary>
        /// Representa a data em que o evento ocorreu
        /// </summary>
        [Required]
        [ValidDate]
        public DateTime data { get; set; }

        /// <summary>
        /// Representa o item associado ao evento
        /// </summary>
        [Required]
        public EventoItemViewModel Item { get; set; }

        /// <summary>
        /// Representa o usuário associado ao evento
        /// </summary>
        [Required]
        public UsuarioViewModel Usuario { get; set; }

        /// <summary>
        /// Construtor do Objeto
        /// </summary>
        public EventoViewModel()
        {

        }
    }
}
