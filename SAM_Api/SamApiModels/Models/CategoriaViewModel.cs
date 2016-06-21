using System.Collections.Generic;

namespace SamApiModels
{

    public class CategoriaViewModel
    {

        public CategoriaViewModel()
        {
            //Itens = new HashSet<ItemViewModel>();
        }

        public int id { get; set; }
        public string nome { get; set; }
        public int peso { get; set; }

        //public virtual ICollection<ItemViewModel> Itens { get; set; }
    }
}
