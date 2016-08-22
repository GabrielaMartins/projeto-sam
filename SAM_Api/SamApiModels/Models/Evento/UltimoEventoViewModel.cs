using SamApiModels.User;
using System.Collections.Generic;

namespace SamApiModels.Evento
{
    public class UltimoEventoViewModel
    {
        public EventoViewModel Evento {get; set;}

        public List<UsuarioViewModel> UsuariosQueFizeram { get; set; }
    }
}
