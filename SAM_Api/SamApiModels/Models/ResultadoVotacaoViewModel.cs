using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
