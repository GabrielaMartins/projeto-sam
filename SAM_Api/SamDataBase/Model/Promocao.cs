using SamDataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamApiModels
{
    public class Promocao
    {
        public Usuario Usuario { get; set; }

        public int PontosFaltantes { get; set; }

        public Promocao()
        {

        }

    }
}
