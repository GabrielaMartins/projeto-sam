using System;
using System.Collections.Generic;

namespace SamApiModels
{

    public class EventoViewModel
    {

        public EventoViewModel()
        {
          
        }

        public int id { get; set; }

        public Nullable<int> item { get; set; }

        public Nullable<int> usuario { get; set; }

        public System.DateTime data { get; set; }

        public bool estado { get; set; }

        public string tipo { get; set; }

        public ItemViewModel Item { get; set; }

        public UsuarioViewModel Usuario { get; set; }

    }
}
