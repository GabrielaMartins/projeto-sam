using SamApiModels.Evento;
using SamApiModels.User;
using SamModelValidationRules.Attributes.Validation;
using System.ComponentModel.DataAnnotations;

namespace SamApiModels.Votacao
{
    /// <summary>
    /// Representa o voto de um usuário em um evento do SAM
    /// </summary>
    public class VotoViewModel
    {
        /// <summary>
        /// Identifica o usuário que votou
        /// </summary>
        [Required]
        public UsuarioViewModel Usuario { get; set; }

        /// <summary>
        /// Identifica o evento que foi votado
        /// </summary>
        [Required]
        public EventoViewModel Evento { get; set; }

        /// <summary>
        /// É o valor que o usuário deu para o item desse evento
        /// </summary>
        [Required]
        [AllowedValues(new[] { 1, 3, 8 })]
        public int Dificuldade { get; set; }

        /// <summary>
        /// É o peso que o usuário deu para o item desse evento
        /// </summary>
        [Required]
        [AllowedValues(new[] { 1, 2, 3, 8 })]
        public int Modificador { get; set; }

        /// <summary>
        /// Construtor do objeto
        /// </summary>
        public VotoViewModel()
        {

        }
    }
}