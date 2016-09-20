using SamModelValidationRules.Attributes.Validation;
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
        /// String codificada na base64 cujo o formato é <!-- <data:image/<imgType>;base64,><bytesEncoded> -->
        /// </summary>
        [Required]
        [ValidPicture]
        public string foto { get; set; }

        /// <summary>
        /// Objeto do tipo Date que representa data de início do usuário no SAM
        /// </summary>
        [Required]
        [DataType("System.DateTime")]
        public DateTime dataInicio { get; set; }


        /// <summary>
        /// Identifica o cargo atual do usuário
        /// </summary>
        [Required]
        [ValidKey(ValidKeyAttribute.Entities.Cargo)]
        public int cargo { get; set; }

        /// <summary>
        /// Identifica o usuário no SAM, o samaccout é um dado que vem do ActiveDirectory da Opus
        /// </summary>
        [Required]
        [ValidKey(ValidKeyAttribute.Entities.Usuario)]
        [StringLength(50, ErrorMessage = "string size is greater than 50 characters")]
        public string samaccount { get; set; }

        /// <summary>
        /// Nome completo do usuário
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "string size is greater than 50 characters")]
        public string nome { get; set; }

        /// <summary>
        /// Indica o perfil do usuário, impactanto nas regras de acesso. Aceita os valores (funcionario, rh)
        /// </summary>
        [Required]
        [AllowedValues(new object[] { "rh", "funcionario" })]
        public string perfil { get; set; }

        /// <summary>
        /// Descrição do usuário como hobs por exemplo
        /// </summary>
        [StringLength(200, ErrorMessage = "string size is greater than 200 characters")]
        public string descricao { get; set; }

        /// <summary>
        /// Facebook
        /// </summary>
        [StringLength(100, ErrorMessage = "string size is greater than 100 characters")]
        public string facebook { get; set; }

        /// <summary>
        /// Github
        /// </summary>
        [StringLength(100, ErrorMessage = "string size is greater than 100 characters")]
        public string github { get; set; }

        /// <summary>
        /// Linkedin
        /// </summary>
        [StringLength(100, ErrorMessage = "string size is greater than 100 characters")]
        public string linkedin { get; set; }

        /// <summary>
        /// Quantidade de pontos que o usuário obtém
        /// </summary>
        [Required]
        public int pontos { get; set; }

        /// <summary>
        /// Construtor do objeto
        /// </summary>
        public AddUsuarioViewModel()
        {
           
        }
    }
}
