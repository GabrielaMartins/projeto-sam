using SamApiModels.User;

namespace SamApiModels.Event
{

    public partial class EventoPendenciaViewModel
    {
        public int id { get; set; }

        public bool estado { get; set; }

        public virtual UsuarioViewModel Usuario { get; set; }
    }
}
