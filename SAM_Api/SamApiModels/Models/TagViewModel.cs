using System.Collections.Generic;

namespace SamApiModels
{

    public partial class TagViewModel
    {
        
        public TagViewModel()
        {
            TaggedItens = new HashSet<ItensTaggedViewModel>();
        }

        public int id { get; set; }

        public string descricao { get; set; }

        public virtual ICollection<ItensTaggedViewModel> TaggedItens { get; set; }
    }
}
