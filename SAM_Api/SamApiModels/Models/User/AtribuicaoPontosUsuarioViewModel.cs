using SamModelValidationRules.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamApiModels.Models.User
{
    /// <summary>
    /// Representa os dados para atribuição de pontos a um usuário
    /// </summary>
    public class AtribuicaoPontosUsuarioViewModel
    {
        /// <summary>
        /// Representa o usuário que irá receber os pontos
        /// </summary>
        [Required]
        [ValidKey(ValidKeyAttribute.Entities.Usuario)]
        public string Usuario { get; set; }

        /// <summary>
        /// Representa o evento pelo qual o usuário recebeu os pontos
        /// </summary>
        [Required]
        [ValidKey(ValidKeyAttribute.Entities.Evento)]
        public int Evento { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public AtribuicaoPontosUsuarioViewModel()
        {

        }
    }
}
