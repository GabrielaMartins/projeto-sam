using SamApiModels.Pendencia;

namespace SamApiModels.User
{
    public class PendenciaUsuarioViewModel
    {
        public int id { get; set; }

        public UsuarioViewModel Usuario;

        public PendenciaEventoViewModel Evento;

        public PendenciaUsuarioViewModel()
        {

        }

    }
}
