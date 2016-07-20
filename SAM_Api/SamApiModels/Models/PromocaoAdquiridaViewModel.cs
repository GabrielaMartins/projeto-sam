using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamApiModels
{
    public class PromocaoAdquiridaViewModel
    {

        public UsuarioViewModel Usuario { get; set; }

        public CargoViewModel Cargo { get; set; }

        public CargoViewModel CargoAnterior { get; set; }

        public DateTime Data { get; set; }

        public PromocaoAdquiridaViewModel()
        {

        }

    }
}
