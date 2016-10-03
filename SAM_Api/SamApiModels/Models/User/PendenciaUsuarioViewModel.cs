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
        [ValidKey(ValidKeyAttribute.Entities.Pendencia, ErrorMessage = "Invalid value supplied to PendenciaUsuarioViewModel.Id. Check if it's a valid key")]
        public int Id { get; set; }

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
        /// Representa o estado da pendência, ou seja, resolvida ou não
        /// </summary>
        [Required]
        public bool Estado;
        

        /// <summary>
        /// construtor do objeto
        /// </summary>
        public PendenciaUsuarioViewModel()
        {

        }

    }
}
