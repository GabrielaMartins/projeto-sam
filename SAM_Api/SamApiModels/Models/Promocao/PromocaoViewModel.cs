using SamApiModels.User;

namespace SamApiModels.Promocao
{
    public class ProximaPromocaoViewModel
    {
        public UsuarioViewModel Usuario { get; set; }

        public int PontosFaltantes { get; set; }

        public ProximaPromocaoViewModel()
        {

        }
    }
}
