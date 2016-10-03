using SamModelValidationRules.Attributes.Validation;
using System.ComponentModel.DataAnnotations;

namespace SamApiModels.Agendamento
{
    /// <summary>
    /// Representa os dados da aprovação de um agendamento
    /// </summary>
    public class AprovaAgendamentoViewModel
    {
        /// <summary>
        /// Representa o evento de agendamento
        /// </summary>
        [Required]
        [ValidKey(ValidKeyAttribute.Entities.Evento, ErrorMessage = "Invalid value supplied to 'AprovaAgendamentoViewModel.Evento'. Check if it's a valid key")]
        public int Evento { get; set; }

        /// <summary>
        /// Indica se aprova ou não esse evento
        /// </summary>
        [Required]
        public bool Aprova { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public AprovaAgendamentoViewModel()
        {

        }
    }
}
