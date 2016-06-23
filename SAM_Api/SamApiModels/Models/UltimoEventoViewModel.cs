using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamApiModels
{
    public class UltimoEventoViewModel
    {
        public EventoViewModel Evento {get; set;}

        public List<UsuarioViewModel> UsuariosQueFizeram { get; set; }
    }
}
