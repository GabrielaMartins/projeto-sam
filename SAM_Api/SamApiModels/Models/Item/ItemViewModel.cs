using SamApiModels.Categoria;
using SamApiModels.User;
using SamModelValidationRules.Attributes.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SamApiModels.Item
{
    /// <summary>
    /// Representa um item do SAM
    /// </summary>
    public class ItemViewModel
    {
        /// <summary>
        /// Identifica um item do SAM
        /// </summary>
        [Required]
        [ValidKey(ValidKeyAttribute.Entities.Item, ErrorMessage = "Invalid value supplied to 'ItemViewModel.id'. Check if it's a valid key")]
        public int id { get; set; }

        /// <summary>
        /// Representa o nome do item
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Invalid value supplied to 'ItemViewModel.nome'. String size is greater than 50 characters")]
        public string nome { get; set; }

        /// <summary>
        /// Representa a descrição do item
        /// </summary>
        [Required]
        [StringLength(200, ErrorMessage = "Invalid value supplied to 'ItemViewModel.descricao'. String size is greater than 200 characters")]
        public string descricao { get; set; }

        /// <summary>
        /// É o valor da dificuldade do item
        /// </summary>
        [Required]
        [AllowedValues(new object[] { 1, 3, 8 }, ErrorMessage = "Invalid value supplied to 'ItemViewModel.dificuldade'. Valid values (1, 3, 8)")]
        public int dificuldade { get; set; }

        /// <summary>
        /// É o peso dado ao item
        /// </summary>
        [Required]
        [AllowedValues(new object[] { 1, 2, 3, 8 }, ErrorMessage = "Invalid value supplied to 'ItemViewModel.modificador'. Valid values (1, 2, 3, 8)")]
        public int modificador { get; set; }

        /// <summary>
        /// Representa a categoria do item
        /// </summary>
        [Required]
        public CategoriaViewModel Categoria { get; set; }

        /// <summary>
        /// Representa todos os usuário que fizeram esse item
        /// </summary>
        [Required]
        public List<UsuarioViewModel> Usuarios { get; set; }

        /// <summary>
        /// Construtor do objeto
        /// </summary>
        public ItemViewModel()
        {

        }
    }

}
