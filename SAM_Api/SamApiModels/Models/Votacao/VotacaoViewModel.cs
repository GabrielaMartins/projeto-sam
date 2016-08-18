using SamApiModels.Evento;
using SamApiModels.User;
using System.Collections.Generic;


namespace SamApiModels.Votacao
{
    public class VotacaoViewModel
    {
        public List<VotoViewModel> Votos { get; set; }

        public EventoViewModel Evento { get; set; }

        public VotacaoViewModel()
        {

        }
    }
}
