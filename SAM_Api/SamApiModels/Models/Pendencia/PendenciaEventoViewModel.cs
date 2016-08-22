using SamApiModels.Item;
using System;

namespace SamApiModels.Pendencia
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
