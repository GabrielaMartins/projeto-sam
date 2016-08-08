using SamApiModels.User;
using System;

namespace SamApiModels.Evento
{

    public class EventoViewModel
    {

        public EventoViewModel()
        {
          
        }

        public int id { get; set; }

        public bool estado { get; set; }

        public string tipo { get; set; }

        public DateTime data { get; set; }

        public EventoItemViewModel Item { get; set; }

        public UsuarioViewModel Usuario { get; set; }

    }
}
