using SamApiModels.Cargo;
using SamModelValidationRules.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SamApiModels.User
{
    /// <summary>
    /// Representa os dados de um usuário do SAM
    /// </summary>
    public class UsuarioViewModel
    {
        
        /// <summary>
        /// Identifica o usuário no SAM
        /// </summary>
        [Required]
        public int id { get; set; }

        /// <summary>
        /// Identifica o usuário pelo samaccount (usuario da rede interna da opus)
        /// </summary>
        [Required]
        public string samaccount { get; set; }

        /// <summary>
        /// Nome completo do usuário
        /// </summary>
        [Required]
        public string nome { get; set; }

        /// <summary>
        /// Objeto do tipo Date que representa data de início do usuário no SAM
        /// </summary>
        [Required]
        [DataType("System.DateTime")]
        public DateTime dataInicio { get; set; }

        /// <summary>
        /// Quantidade de pontos que o usuário obtém
        /// </summary>
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
        /// Endereço da imagem no servidor
        /// </summary>
        [Required]
        [StringLength(300, ErrorMessage = "string size is greater than 300 characters")]
        public string foto { get; set; }

        /// <summary>
        /// Diz se o usuário está ou não ativo no SAM
        /// </summary>
        [Required]
        public bool ativo { get; set; }

        /// <summary>
        /// Indica o perfil do usuário, impactanto nas regras de acesso. Aceita os valores (funcionario, rh)
        /// </summary>
        [Required]
        [AllowedValues(new[] { "rh", "funcionario" })]
        public string perfil { get; set; }

        /// <summary>
        /// Representa o cargo atual do usuário
        /// </summary>
        [Required]
        public virtual CargoViewModel Cargo { get; set; }

        /// <summary>
        /// É uma lista dos próximos cargos na hierarquia de cargo, os quais o usuário pode escolher
        /// </summary>
        [Required]
        public virtual List<CargoViewModel> ProximoCargo { get; set; }

        /// <summary>
        /// Construtor do objeto
        /// </summary>
        public UsuarioViewModel()
        {
          
            pontos = 0;
            Cargo = new CargoViewModel();
            ProximoCargo = new List<CargoViewModel>();
        }

    }
}
