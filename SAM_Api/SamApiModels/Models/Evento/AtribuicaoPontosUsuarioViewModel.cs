using SamModelValidationRules.Attributes.Validation;
using System.ComponentModel.DataAnnotations;

namespace SamApiModels.Evento
{
    /// <summary>
    /// Representa os dados para atribuição de pontos a um usuário
    /// </summary>
    public class AtribuicaoPontosUsuarioViewModel
    {
        /// <summary>
        /// Representa se o usuário irá receber os pontos
        /// </summary>
        public bool ReceberPontos { get; set; }

        /// <summary>
        /// Representa o usuário que irá receber os pontos
        /// </summary>
        [Required]
        [ValidKey(ValidKeyAttribute.Entities.Usuario, ErrorMessage = "Invalid value supplied to 'AtribuicaoPontosUsuarioViewModel.Usuario'. Check if it's a valid key")]
        public string Usuario { get; set; }

        /// <summary>
        /// Representa o evento pelo qual o usuário recebeu os pontos
        /// </summary>
        [Required]
        [ValidKey(ValidKeyAttribute.Entities.Evento, ErrorMessage = "Invalid value supplied to 'AtribuicaoPontosUsuarioViewModel.Evento'. Check if it's a valid key")]
        public int Evento { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public AtribuicaoPontosUsuarioViewModel()
        {
            ReceberPontos = true;
        }
    }
}
