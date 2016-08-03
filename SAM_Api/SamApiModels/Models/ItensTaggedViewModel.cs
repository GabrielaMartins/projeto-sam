using System;

namespace SamApiModels
{

    public partial class ItensTaggedViewModel
    {
        public int id { get; set; }
        
        public virtual TagViewModel Tag { get; set; }

        //public virtual ItemViewModel Item { get; set; }
    }
}
