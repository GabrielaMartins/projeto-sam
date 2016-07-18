using System;
using System.Collections.Generic;

namespace SamApiModels
{

    public partial class UsuarioViewModel
    {
        
        public UsuarioViewModel()
        {
            
        }

        public int id { get; set; }

        public int pontos { get; set; }

        public string samaccount { get; set; }

        public string nome { get; set; }

        public string descricao { get; set; }

        public string[] redes { get; set; }

        public DateTime dataInicio { get; set; }

        public string imagem { get; set; }

        public bool ativo { get; set; }

        public string perfil { get; set; }

        public virtual CargoViewModel Cargo { get; set; }

        public virtual List<CargoViewModel> ProximoCargo { get; set; }
    }
}
