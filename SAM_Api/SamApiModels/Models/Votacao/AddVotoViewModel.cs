using SamModelValidationRules.Attributes.Validation;
using System.ComponentModel.DataAnnotations;

namespace SamApiModels.Votacao
{
    /// <summary>
    /// Representa a adição de um novo voto no SAM
    /// </summary>
    public class AddVotoViewModel
    {
        /// <summary>
        /// É o samaccount do usuário que votou no evento em questão
        /// </summary>
        [Required]
        [ValidKey(ValidKeyAttribute.Entities.Usuario)]
        public string Usuario { get; set; }

        /// <summary>
        /// É o id do evento que estã em votação
        /// </summary>
        [Required]
        [ValidKey(ValidKeyAttribute.Entities.Evento)]
        public int Evento { get; set; }

        /// <summary>
        /// Valor que compõe a nota da votação (1, 3, 8)
        /// </summary>
        [AllowedValues(new object[] {1, 3, 8 })]
        [Required]
        public int Dificuldade { get; set; }

        /// <summary>
        /// Valor que compõe a nota da votação (1, 2, 3, 8)
        /// </summary>
        [AllowedValues(new object[] {1, 2, 3, 8 })]
        [Required]
        public int Modificador { get; set; }

        /// <summary>
        /// construtor da classe
        /// </summary>
        public AddVotoViewModel()
        {
          
        }
    }
}
