﻿using SamModelValidationRules.Attributes.Validation;
using System.ComponentModel.DataAnnotations;

namespace SamApiModels.Votacao
{
    public class AddVotoViewModel
    {
        /// <summary>
        /// É o samaccount do usuário que votou no evento em questão
        /// </summary>
        [Required]
        public string Usuario { get; set; }

        /// <summary>
        /// É o id do evento que estã em votação
        /// </summary>
        [Required]
        public int Evento { get; set; }

        /// <summary>
        /// Valor que compõe a nota da votação (1, 3, 8)
        /// </summary>
        [AllowedValues(new int[] {1, 3, 8 })]
        [Required]
        public int Dificuldade { get; set; }

        /// <summary>
        /// Valor que compõe a nota da votação (1, 2, 3, 8)
        /// </summary>
        [AllowedValues(new int[] {1, 2, 3, 8 })]
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
