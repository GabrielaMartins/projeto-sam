using SamApiModels.User;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SamApiModels.Evento
{
    /// <summary>
    /// Representa um evento do SAM associado aos usuários que o fizeram
    /// </summary>
    public class UltimoEventoViewModel
    {
        /// <summary>
        /// Representa o evento do SAM
        /// </summary>
        [Required]
        public EventoViewModel Evento {get; set;}

        /// <summary>
        /// Representa todos os usuário que já fizeram esse evento
        /// </summary>
        [Required]
        public List<UsuarioViewModel> UsuariosQueFizeram { get; set; }
    }
}
