using System;

namespace SamApiModels
{

    public partial class ItensTaggedViewModel
    {
        public int id { get; set; }

        public Nullable<int> item { get; set; }

        public Nullable<int> tag { get; set; }

        public virtual ItemViewModel Item { get; set; }

        public virtual TagViewModel Tag { get; set; }
    }
}
