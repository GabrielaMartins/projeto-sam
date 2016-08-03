using System;
using System.Collections.Generic;

namespace SamApiModels.Event
{

    public class ItemEventoViewModel
    {

        public ItemEventoViewModel()
        {
           
        }

        public int id { get; set; }

        public string nome { get; set; }

        public string descricao { get; set; }

        public int dificuldade { get; set; }

        public int modificador { get; set; }

        public CategoriaViewModel Categoria { get; set; }

    }
}
