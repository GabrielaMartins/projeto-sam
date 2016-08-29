<<<<<<< HEAD
﻿using SamModelValidationRules.Attributes.Validation;
using System;
=======
﻿using Opus.DataBaseEnvironment;
using SamDataBase.Model;
using SamModelValidationRules.Attributes.Validation;
using System;
using System.ComponentModel.DataAnnotations;
>>>>>>> master

namespace SamApiModels.Models.User
{
    public class UpdateUsuarioViewModel
    {

<<<<<<< HEAD
=======
        [Required]
>>>>>>> master
        public string nome { get; set; }

        public DateTime dataInicio { get; set; }

        public int pontos { get; set; }

        public string descricao { get; set; }

        public string facebook { get; set; }

        public string github { get; set; }

        public string linkedin { get; set; }

<<<<<<< HEAD
        public string foto { get; set; }

        [AllowedValues(new[] { "RH", "Funcionario" })]
        public string perfil { get; set; }

=======
        [Required]
        [ValidPicture]
        public string foto { get; set; }

        [Required]
        [AllowedValues(new[] { "RH", "Funcionario" })]
        public string perfil { get; set; }

        //[ValidForeignKey()]
>>>>>>> master
        public int cargo { get; set; }

        public UpdateUsuarioViewModel()
        {

        }
    }
}
