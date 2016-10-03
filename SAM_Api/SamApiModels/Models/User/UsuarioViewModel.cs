using SamApiModels.Cargo;
using SamModelValidationRules.Attributes.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SamApiModels.User
{
    /// <summary>
    /// Representa os dados de um usu�rio do SAM
    /// </summary>
    public class UsuarioViewModel
    {
        
        /// <summary>
        /// Identifica o usu�rio no SAM
        /// </summary>
        [Required]
        public int id { get; set; }

        /// <summary>
        /// Identifica o usu�rio pelo samaccount (usuario da rede interna da opus)
        /// </summary>
        [Required]
        public string samaccount { get; set; }

        /// <summary>
        /// Nome completo do usu�rio
        /// </summary>
        [Required]
        public string nome { get; set; }

        /// <summary>
        /// Objeto do tipo Date que representa data de in�cio do usu�rio no SAM
        /// </summary>
        [Required]
        [DataType("System.DateTime")]
        public DateTime dataInicio { get; set; }

        /// <summary>
        /// Quantidade de pontos que o usu�rio obt�m
        /// </summary>
        [Required]
        public int pontos { get; set; }

        /// <summary>
        /// Descri��o do usu�rio como hobs por exemplo
        /// </summary>
        [Required]
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
        /// Endere�o da imagem no servidor
        /// </summary>
        [Required]
        public string foto { get; set; }

        /// <summary>
        /// Diz se o usu�rio est� ou n�o ativo no SAM
        /// </summary>
        [Required]
        public bool ativo { get; set; }

        /// <summary>
        /// Indica o perfil do usu�rio, impactanto nas regras de acesso. Aceita os valores (funcionario, rh)
        /// </summary>
        [Required]
        public string perfil { get; set; }

        /// <summary>
        /// Representa o cargo atual do usu�rio
        /// </summary>
        [Required]
        public virtual CargoViewModel Cargo { get; set; }

        /// <summary>
        /// Data da ultima promocao do usu�rio
        /// </summary>
        [Required]
        public DateTime DataUltimaPromocao { get; set; }


        /// <summary>
        /// � uma lista dos pr�ximos cargos na hierarquia de cargo, os quais o usu�rio pode escolher
        /// </summary>
        [Required]
        public List<CargoViewModel> ProximoCargo { get; set; }

    
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
