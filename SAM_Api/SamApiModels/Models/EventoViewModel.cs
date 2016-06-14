using System;
using System.Collections.Generic;

namespace SamApiModels
{

    public class EventoViewModel
    {

        public EventoViewModel()
        {
            Pendencias = new HashSet<PendenciaViewModel>();
            ResultadoVotacoes = new HashSet<ResultadoVotacoesViewModel>();
        }

        public int id { get; set; }

        public Nullable<int> item { get; set; }

        public Nullable<int> usuario { get; set; }

        public System.DateTime data { get; set; }

        public bool estado { get; set; }

        public int tipo { get; set; }

        public virtual ItemViewModel Item { get; set; }

        public virtual UsuarioViewModel Usuario { get; set; }

        public virtual ICollection<PendenciaViewModel> Pendencias { get; set; }

        public virtual ICollection<ResultadoVotacoesViewModel> ResultadoVotacoes { get; set; }
    }
}
