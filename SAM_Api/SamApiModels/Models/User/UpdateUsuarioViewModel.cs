using Opus.DataBaseEnvironment;
using SamDataBase.Model;
using SamModelValidationRules.Attributes.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace SamApiModels.Models.User
{
    public class UpdateUsuarioViewModel
    {

        [Required]
        public string nome { get; set; }

        [Required]
        public DateTime dataInicio { get; set; }

        [Required]
        public int pontos { get; set; }

        [Required]
        public string descricao { get; set; }

        public string facebook { get; set; }

        public string github { get; set; }

        public string linkedin { get; set; }

        [Required]
        [ValidPicture]
        public string foto { get; set; }

        [Required]
        [AllowedValues(new[] { "RH", "Funcionario" })]
        public string perfil { get; set; }

        //[ValidForeignKey()]
        public int cargo { get; set; }

        public UpdateUsuarioViewModel()
        {

        }
    }
}
