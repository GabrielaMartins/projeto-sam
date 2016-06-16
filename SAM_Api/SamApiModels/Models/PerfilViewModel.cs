using SamDataBase.Model;
using System.Collections.Generic;

namespace SamApiModels
{
    public class PerfilViewModel
    {
        public UsuarioViewModel Usuario { get; set; }
        public List<Evento> Eventos { get; set; }

        public PerfilViewModel()
        {

        }

    }
}
