using SamApiModels.Cargo;
using SamApiModels.User;
using SamModelValidationRules.Attributes.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace SamApiModels.Promocao
{
    /// <summary>
    /// Representa uma promoção adquirida por um funcionário do SAM
    /// </summary>
    public class PromocaoAdquiridaViewModel
    {
        /// <summary>
        /// Representa o funcionário que adquiriu a promoção
        /// </summary>
        [Required]
        public UsuarioViewModel Usuario { get; set; }

        /// <summary>
        /// Representa o cargo adquirido
        /// </summary>
        [Required]
        public CargoViewModel CargoAdquirido { get; set; }

        /// <summary>
        /// Representa o cargo anterior ao cargo adquirido
        /// </summary>
        [Required]
        public CargoViewModel CargoAnterior { get; set; }

        /// <summary>
        /// Representa a data em que o funcionário adquiriu um novo cargo
        /// </summary>
        [Required]
        [ValidDate]
        public DateTime Data { get; set; }

        /// <summary>
        /// Construtor do objeto
        /// </summary>
        public PromocaoAdquiridaViewModel()
        {

        }

    }
}