using System.Collections.Generic;

namespace SamApiModels
{
    public class UltimoEventoViewModel
    {
        public EventoViewModel Evento {get; set;}

        public List<UsuarioViewModel> UsuariosQueFizeram { get; set; }
    }
}
