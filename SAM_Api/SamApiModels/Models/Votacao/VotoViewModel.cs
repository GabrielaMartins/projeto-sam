using SamApiModels.User;

namespace SamApiModels.Votacao
{
    public class VotoViewModel
    {

        public UsuarioViewModel Usuario { get; set; }

        public int Dificuldade { get; set; }

        public int Modificador { get; set; }

        public VotoViewModel()
        {

        }
    }
}
