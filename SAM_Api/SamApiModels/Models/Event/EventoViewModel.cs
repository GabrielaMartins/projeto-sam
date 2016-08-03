using SamApiModels.User;
using System;
using System.Collections.Generic;

namespace SamApiModels.Event
{

    public class EventoViewModel
    {

        public EventoViewModel()
        {
          
        }

        public int id { get; set; }

        public System.DateTime data { get; set; }

        public bool estado { get; set; }

        public string tipo { get; set; }

        public ItemEventoViewModel Item { get; set; }

        public UsuarioViewModel Usuario { get; set; }

        public ICollection<EventoPendenciaViewModel> Pendencias { get; set;}

        //public ICollection<ResultadoVotacoesViewModel> ResultadoVotacoes { get; set; }

    }
}
