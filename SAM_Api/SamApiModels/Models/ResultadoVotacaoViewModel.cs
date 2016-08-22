using SamApiModels.Evento;
using SamApiModels.User;

namespace SamApiModels
{
    public class ResultadoVotacaoViewModel
    {
        public int id { get; set; }

       
        public int dificuldade { get; set; }

        public int modificador { get; set; }

        public virtual EventoViewModel Evento { get; set; }

        public virtual UsuarioViewModel Usuario { get; set; }
    }
}
