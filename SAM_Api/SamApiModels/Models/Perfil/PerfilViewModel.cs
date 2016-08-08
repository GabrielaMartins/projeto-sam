using SamApiModels.Evento;
using SamApiModels.Promocao;
using SamApiModels.User;
using System.Collections.Generic;

namespace SamApiModels.Perfil
{
    public class PerfilViewModel
    {
        public List<PromocaoAdquiridaViewModel> PromocoesAdquiridas { get; set; }

        public UsuarioViewModel Usuario { get; set; }

        public List<EventoViewModel> Atividades { get; set; }

        public PerfilViewModel()
        {

        }

    }
}
