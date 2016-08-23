using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SamApiModels.User
{
    /// <summary>
    /// Representa um novo usuário
    /// </summary>
    public class AddUsuarioViewModel
    {
        /// <summary>
        /// Segue o formato UrlEncoded64
        /// </summary>
        [Required]
        public string foto { get; set; }

        /// <summary>
        /// Objeto do tipo Date que representa data de início desse usuário no SAM
        /// </summary>
        [Required]
        [DataType("System.DateTime")]
        public DateTime dataInicio { get; set; }


        /// <summary>
        /// Identifica o cargo atual do usuário
        /// </summary>
        [Required]
        public int cargo { get; set; }

        /// <summary>
        /// Identifica o usuário no SAM, o samaccout é um dado que vem do ActiveDirectory da Opus
        /// </summary>
        [Required]
        public string samaccount { get; set; }

        /// <summary>
        /// Nome completo do usuário
        /// </summary>
        [Required]
        public string nome { get; set; }

        /// <summary>
        /// Indica o perfil do usuário, impactanto nas regras de acesso. Aceita os valores (Funcionario, RH)
        /// </summary>
        [Required]
        public string perfil { get; set; }

        /// <summary>
        /// Descrição do usuário como hobs por exemplo
        /// </summary>
        public string descricao { get; set; }

        /// <summary>
        /// Facebook
        /// </summary>
        public string facebook { get; set; }

        /// <summary>
        /// Github
        /// </summary>
        public string github { get; set; }

        /// <summary>
        /// Linkedin
        /// </summary>
        public string linkedin { get; set; }

        /// <summary>
        /// Quantidade de pontos que o usuário obtém
        /// </summary>
        public int pontos { get; set; }

        public AddUsuarioViewModel()
        {
           
        }
    }
}
