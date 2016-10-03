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
    public class AddItemViewModel
    {

        /// <summary>
        /// Representa o nome do item
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Invalid value supplied to 'AddItemViewModel.nome'. String size is greater than 50 characters")]
        public string Nome { get; set; }

        /// <summary>
        /// Representa a descrição do item
        /// </summary>
        [Required]
        [StringLength(200, ErrorMessage = "Invalid value supplied to 'AddItemViewModel.Descricao'. String size is greater than 200 characters")]
        public string Descricao { get; set; }

        /// <summary>
        /// É o valor da dificuldade do item
        /// </summary>
        [Required]
        [AllowedValues(new object[] { 1, 3, 8 }, ErrorMessage = "Invalid value supplied to 'AddItemViewModel.Dificuldade'. Valid values (1, 3, 8)")]
        public int Dificuldade { get; set; }

        /// <summary>
        /// É o peso dado ao item
        /// </summary>
        [Required]
        [AllowedValues(new object[] { 1, 3, 8 }, ErrorMessage = "Invalid value supplied to 'AddItemViewModel.Modificador'. Valid values (1, 3, 8)")]
        public int Modificador { get; set; }

        /// <summary>
        /// Itentifica a categoria do item
        /// </summary>
        [Required]
        [ValidKey(ValidKeyAttribute.Entities.Categoria, ErrorMessage = "Invalid value supplied to 'AddItemViewModel.Categoria'. Check if it's a valid key")]
        public int Categoria { get; set; }


        /// <summary>
        /// Construtor do objeto
        /// </summary>
        public AddItemViewModel()
        {

        }
    }

}
