using SamModelValidationRules.Attributes.Validation;
using System.ComponentModel.DataAnnotations;

namespace SamApiModels.Agendamento
{
    /// <summary>
    /// Representa os dados do agendamento de um evento
    /// </summary>
    public class AgendamentoViewModel
    {
        /// <summary>
        /// Identifica o item associado ao agendamento
        /// </summary>
        [Required]
        [ValidKey(ValidKeyAttribute.Entities.Item, ErrorMessage = "Invalid value supplied to 'AgendamentoViewModel.Item'. Check if it's a valid key")]
        public int Item { get; set; }

        /// <summary>
        /// Identifica o funcionário requerendo o agendamento
        /// </summary>
        [Required]
        [ValidKey(ValidKeyAttribute.Entities.Usuario, ErrorMessage = "Invalid value supplied to 'AgendamentoViewModel.Funcionario'. Check if it's a valid key")]
        public string Funcionario { get; set; }

        /// <summary>
        /// Data na qual irá ocorrer o evento
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [RegularExpression(@"^\d{2}/\d{2}/\d{4}$", ErrorMessage = "Invalid value supplied to 'AgendamentoViewModel.Data'. Invalid date format")]
        public string Data { get; set; }

        /// <summary>
        /// Construtor do objeto
        /// </summary>
        public AgendamentoViewModel()
        {

        }

    }
}