using SamApiModels.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamApiModels.Pendency
{
    public class PendenciaEventoViewModel
    {
        public int id { get; set; }


        public bool estado { get; set; }

        public string tipo { get; set; }

        public DateTime data { get; set; }

        public ItemViewModel Item { get; set; }

        public PendenciaEventoViewModel()
        {

        }
    }
}
