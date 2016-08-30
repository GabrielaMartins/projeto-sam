using SamApiModels.Pendencia;
using SamModelValidationRules.Attributes.Validation;
using System.ComponentModel.DataAnnotations;

namespace SamApiModels.User
{
    /// <summary>
    /// Representa a pendência associada a um usuário do SAM
    /// </summary>
    public class PendenciaUsuarioViewModel
    {
        /// <summary>
        /// Identifica a pendência do usuário
        /// </summary>
        [Required]
        [ValidKey(ValidKeyAttribute.Entities.Pendencia)]
        public int id { get; set; }

        /// <summary>
        /// Representa o usuário cujo a pendência foi destinada
        /// </summary>
        [Required]
        public UsuarioViewModel Usuario;

        /// <summary>
        /// Representa o evento para o qual a pendência foi gerada
        /// </summary>
        [Required]
        public PendenciaEventoViewModel Evento;

        /// <summary>
        /// construtor do objeto
        /// </summary>
        public PendenciaUsuarioViewModel()
        {

        }

    }
}
