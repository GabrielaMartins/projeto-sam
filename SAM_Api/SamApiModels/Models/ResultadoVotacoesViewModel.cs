using SamApiModels;
using System;

namespace SamApiModels
{

    public partial class ResultadoVotacoesViewModel
    {
        public int id { get; set; }

        public Nullable<int> evento { get; set; }

        public Nullable<int> usuario { get; set; }

        public int dificuldade { get; set; }

        public int modificador { get; set; }


        public virtual EventoViewModel Evento { get; set; }

        public virtual UsuarioViewModel Usuario { get; set; }
    }
}
