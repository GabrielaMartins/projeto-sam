using SamModelValidationRules.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamApiModels.Models.Votacao
{
    /// <summary>
    /// Representa o encerramento de uma votação
    /// </summary>
    public class CloseVotacaoViewModel
    {
        /// <summary>
        /// Representa o evento da votação
        /// </summary>
        [Required]
        [ValidKey(ValidKeyAttribute.Entities.Evento, ErrorMessage = "Invalid value supplied to 'CloseVotacaoViewModel.Evento'. Check if it's a valid key")]
        public int Evento { get; set; }

        /// <summary>
        /// É a dificuldade atribuída ao item da votação
        /// </summary>
        [Required]
        [AllowedValues(new object[] { 1, 3, 8 }, ErrorMessage = "Invalid value supplied to 'CloseVotacaoViewModel.Dificuldade'. Valid values (1, 3, 8)")]
        public int Dificuldade { get; set; }

        /// <summary>
        /// É o modificador (raso, profundo, alinhado ou não alinhado) atribuído ao item da votação
        /// </summary>
        [Required]
        [AllowedValues(new object[] { 1, 2, 3, 8 }, ErrorMessage = "Invalid value supplied to 'CloseVotacaoViewModel.Modificador'. Valid values (1, 2, 3, 8)")]
        public int Modificador { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public CloseVotacaoViewModel()
        {

        }
    }
}
