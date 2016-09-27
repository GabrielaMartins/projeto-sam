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
        [ValidKey(ValidKeyAttribute.Entities.Item)]
        public int Item { get; set; }

        /// <summary>
        /// Identifica o funcionário requerendo o agendamento
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "string size is greater than 50 characters")]
        public string Funcionario { get; set; }

        /// <summary>
        /// Data na qual irá ocorrer o evento
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [ValidDate]
        public string Data { get; set; }

        /// <summary>
        /// Construtor do objeto
        /// </summary>
        public AgendamentoViewModel()
        {

        }

    }
}