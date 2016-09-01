using SamApiModels.Evento;
using SamApiModels.Votacao;
using System.Collections.Generic;


namespace SamApiModels.Models.Votacao
{
    /// <summary>
    /// Representa os dados de uma votação no SAM
    /// </summary>
    public class VotacaoViewModel
    {
        /// <summary>
        /// Representa o evento que foi votado
        /// </summary>
        public EventoViewModel Evento { get; set; }

        /// <summary>
        /// Representa os votos dos usuários nesse evento
        /// </summary>
        public List<VotoViewModel> Votos {get; set;}

        /// <summary>
        /// Construtor do objeto
        /// </summary>
        public VotacaoViewModel()
        {

        }
    }
}
