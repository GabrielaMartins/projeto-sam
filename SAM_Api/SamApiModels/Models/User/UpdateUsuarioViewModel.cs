using SamModelValidationRules.Attributes.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace SamApiModels.Models.User
{
    /// <summary>
    /// Representa o modelo de dados para atualizar um usuário do SAM
    /// </summary>
    public class UpdateUsuarioViewModel
    {
        /*
        /// <summary>
        /// Nome completo do usuário do SAM
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "string size is greater than 50 characters")]
        public string nome { get; set; }
        */

        /// <summary>
        /// Identifica a data que o funcionário iniciou no SAM
        /// </summary>
        [Required]
        [ValidDate]
        public string dataInicio { get; set; }

        /// <summary>
        /// Representa a quantidade de pontos adquiridas pelo funcionário no SAM
        /// </summary>
        [Required]
        public int pontos { get; set; }

        /// <summary>
        /// Descrição do usuário como, hobs, gostos e etc.
        /// </summary>
        [Required]
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
        /// String codificada na base64 cujo o formato é <!-- <data:image/<imgType>;base64,><bytesEncoded> -->
        /// </summary>
        [Required]
        [ValidPicture]
        public string foto { get; set; }

        /// <summary>
        /// Indica o perfil do usuário, impactanto nas regras de acesso. Aceita os valores (funcionario, rh)
        /// </summary>
        [Required]
        [AllowedValues(new object[] { "rh", "funcionario" })]
        public string perfil { get; set; }

        /// <summary>
        /// Identifica o cargo atual do funcionário
        /// </summary>
        [ValidKey(ValidKeyAttribute.Entities.Cargo)]
        public int cargo { get; set; }

        /// <summary>
        /// Construtor do objeto
        /// </summary>
        public UpdateUsuarioViewModel()
        {

        }
    }
}
