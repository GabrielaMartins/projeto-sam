using SamModelValidationRules.Attributes.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace SamApiModels.Models.User
{
    public class UpdateUsuarioViewModel
    {

        [Required]
        [StringLength(50, ErrorMessage = "string size is greater than 50 characters")]
        public string nome { get; set; }

        [Required]
        public DateTime dataInicio { get; set; }

        [Required]
        public int pontos { get; set; }

        /// <summary>
        /// Descrição do usuário como hobs por exemplo
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
        [StringLength(300, ErrorMessage = "string size is greater than 300 characters")]
        public string foto { get; set; }

        /// <summary>
        /// Indica o perfil do usuário, impactanto nas regras de acesso. Aceita os valores (funcionario, rh)
        /// </summary>
        [Required]
        [AllowedValues(new[] { "rh", "funcionario" })]
        public string perfil { get; set; }

        /// <summary>
        /// Identifica o cargo atual do funcionário
        /// </summary>
        [ValidForeignKey(typeof(SamDataBase.Model.Cargo))]
        public int cargo { get; set; }

        public UpdateUsuarioViewModel()
        {

        }
    }
}
