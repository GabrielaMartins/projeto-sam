using System;
using System.Collections.Generic;

namespace SamApiModels
{

    public class ItemViewModel
    {

        public ItemViewModel()
        {
          
        }

        public int id { get; set; }

        public string nome { get; set; }

        public string descricao { get; set; }

        public int dificuldade { get; set; }

        public int modificador { get; set; }

        public CategoriaViewModel Categoria { get; set; }

        public List<EventoViewModel> Eventos { get; set; }

        public List<ItensTaggedViewModel> TaggedItens { get; set; }

    }
}
