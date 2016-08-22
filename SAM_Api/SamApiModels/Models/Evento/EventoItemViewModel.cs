using SamApiModels.Categoria;
using SamApiModels.User;
using System;
using System.Collections.Generic;

namespace SamApiModels.Evento
{

    public class EventoItemViewModel
    {

        public EventoItemViewModel()
        {
           
        }

        public int id { get; set; }

        public string nome { get; set; }

        public string descricao { get; set; }

        public int dificuldade { get; set; }

        public int modificador { get; set; }

        public CategoriaViewModel Categoria { get; set; }

        public List<UsuarioViewModel> Usuarios { get; set; }
    }
}
