using System.Collections.Generic;

namespace SamApiModels
{
    public class PerfilViewModel
    {
        public UsuarioViewModel Usuario { get; set; }

        public List<EventoViewModel> Eventos { get; set; }

        public PerfilViewModel()
        {

        }

    }
}
