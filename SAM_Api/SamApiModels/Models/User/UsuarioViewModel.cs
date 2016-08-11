using SamApiModels.Cargo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SamApiModels.User
{

    public partial class UsuarioViewModel
    {
        

        [Required]
        public int id { get; set; }

        [Required]
        public string samaccount { get; set; }

        [Required]
        public string nome { get; set; }

        [Required]
        public DateTime dataInicio { get; set; }

        [Required]
        public int pontos { get; set; }

        [Required]
        public string descricao { get; set; }

        [Required]
        public string[] redes { get; set; }

        [Required]
        public string foto { get; set; }

        [Required]
        public bool ativo { get; set; }

        [Required]
        public string perfil { get; set; }

        [Required]
        public virtual CargoViewModel Cargo { get; set; }

        [Required]
        public virtual List<CargoViewModel> ProximoCargo { get; set; }

        public UsuarioViewModel()
        {
            redes = new string[0];
            pontos = 0;
            Cargo = new CargoViewModel();
            ProximoCargo = new List<CargoViewModel>();
        }

    }
}
