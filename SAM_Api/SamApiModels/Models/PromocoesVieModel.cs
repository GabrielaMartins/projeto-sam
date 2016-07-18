using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamApiModels
{
    public class PromocoesViewModel
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string imagem { get; set; }
        public string cargoAtual { get; set; }
        public string proximoCargo { get; set; }
        public int pontosFaltantes { get; set; }

    }
}
