using System;
using System.Collections.Generic;

namespace SamApiModels
{

    public class ItemViewModel
    {

        public ItemViewModel()
        {
            Eventos = new HashSet<EventoViewModel>();
            TaggedItens = new HashSet<ItensTaggedViewModel>();
        }

        public int id { get; set; }

        public string nome { get; set; }

        public string descricao { get; set; }

        public int dificuldade { get; set; }

        public int modificador { get; set; }

        public Nullable<int> categoria { get; set; }

        public virtual CategoriaViewModel Categoria { get; set; }

        public virtual ICollection<EventoViewModel> Eventos { get; set; }

        public virtual ICollection<ItensTaggedViewModel> TaggedItens { get; set; }
    }
}
