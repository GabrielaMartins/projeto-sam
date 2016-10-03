using SamModelValidationRules.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamApiModels.Evento
{
    /// <summary>
    /// Representa a inserção de um novo evento de votação
    /// </summary>
    public class AddEventoVotacaoViewModel
    {
        /// <summary>
        /// Representa o item da votação
        /// </summary>
        [Required]
        [ValidKey(ValidKeyAttribute.Entities.Item, ErrorMessage = "Invalid value supplied to 'AddEventoVotacaoViewModel.Item'. Check if it's a valid key")]
        public int Item { get; set; }

        /// <summary>
        /// Representa o usuário do evento sendo votado
        /// </summary>
        [Required]
        [ValidKey(ValidKeyAttribute.Entities.Usuario, ErrorMessage = "Invalid value supplied to 'AddEventoVotacaoViewModel.Usuario'. Check if it's a valid key")]
        public string Usuario { get; set; }

        /// <summary>
        /// Representa a data que ocorrerá o evento
        /// </summary>
        [Required]
        [RegularExpression(@"^\d{2}/\d{2}/\d{4}$", ErrorMessage = "Invalid value supplied to 'AddEventoVotacaoViewModel.Data'. Invalid date format")]
        public string Data { get; set; }

        /// <summary>
        /// Representa o tipo do evento
        /// </summary>
        [ReadOnly(true)]
        [DefaultValue("votacao")]
        public string tipo { get; private set; }

        /// <summary>
        /// Representa o estado do evento
        /// </summary>
        [ReadOnly(true)]
        [DefaultValue(false)]
        public bool estado { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public AddEventoVotacaoViewModel()
        {
            tipo = "votacao";
            estado = false;
        }
    }
}
