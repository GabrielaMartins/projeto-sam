using System;
using System.Collections.Generic;

namespace SamApiModels
{

    public class CargoViewModel
    {

        public CargoViewModel()
        {
            //this.Cargos = new HashSet<CargoViewModel>();
            //this.Usuarios = new HashSet<UsuarioViewModel>();
        }

        public int id { get; set; }

        public string nome { get; set; }

        public Nullable<int> anterior { get; set; }

        public int pontuacao { get; set; }

        public virtual CargoViewModel CargoAnterior { get; set; }
        
        // public virtual ICollection<CargoViewModel> Cargos { get; set; }
        
        // public virtual ICollection<UsuarioViewModel> Usuarios { get; set; }
    }
}
