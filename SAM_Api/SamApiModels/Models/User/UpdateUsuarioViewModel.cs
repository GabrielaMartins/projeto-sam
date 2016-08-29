using SamModelValidationRules.Attributes.Validation;
using System;

namespace SamApiModels.Models.User
{
    public class UpdateUsuarioViewModel
    {

        public string nome { get; set; }

        public DateTime dataInicio { get; set; }

        public int pontos { get; set; }

        public string descricao { get; set; }

        public string facebook { get; set; }

        public string github { get; set; }

        public string linkedin { get; set; }

        public string foto { get; set; }

        [AllowedValues(new[] { "RH", "Funcionario" })]
        public string perfil { get; set; }

        public int cargo { get; set; }

        public UpdateUsuarioViewModel()
        {

        }
    }
}
