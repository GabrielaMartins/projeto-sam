using SamApiModels.Categoria;
using SamApiModels.User;
using System.Collections.Generic;

namespace SamApiModels.Item
{

    public class ItemViewModel
    {
        public int id { get; set; }

        public string nome { get; set; }

        public string descricao { get; set; }

        public int dificuldade { get; set; }

        public int modificador { get; set; }

        public CategoriaViewModel Categoria { get; set; }

        public List<UsuarioViewModel> Usuarios { get; set; }

        public ItemViewModel()
        {

        }
    }

}
