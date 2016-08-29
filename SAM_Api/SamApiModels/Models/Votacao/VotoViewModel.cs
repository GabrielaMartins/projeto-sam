using SamApiModels.Evento;
using SamApiModels.User;

namespace SamApiModels.Votacao
{
    public class VotoViewModel
    {

        public UsuarioViewModel Usuario { get; set; }

        public EventoViewModel Evento {get; set;}

        public int Dificuldade { get; set; }

        public int Modificador { get; set; }
<<<<<<< HEAD
=======

>>>>>>> master

        public VotoViewModel()
        {

        }
    }
}
