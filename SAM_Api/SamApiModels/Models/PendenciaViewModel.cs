using System;

namespace SamApiModels
{

    public partial class PendenciaViewModel
    {
        public int id { get; set; }

        public bool estado { get; set; }

        public virtual EventoViewModel Evento { get; set; }

        public virtual UsuarioViewModel Usuario { get; set; }
    }
}
