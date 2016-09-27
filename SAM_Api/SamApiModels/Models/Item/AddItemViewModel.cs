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
        [StringLength(50, ErrorMessage = "string size is greater than 50 characters")]
        public string Nome { get; set; }

        /// <summary>
        /// Representa a descrição do item
        /// </summary>
        [Required]
        [StringLength(200, ErrorMessage = "string size is greater than 200 characters")]
        public string Descricao { get; set; }

        /// <summary>
        /// É o valor da dificuldade do item
        /// </summary>
        [Required]
        [AllowedValues(new object[] { 1, 3, 8 })]
        public int Dificuldade { get; set; }

        /// <summary>
        /// É o peso dado ao item
        /// </summary>
        [Required]
        [AllowedValues(new object[] { 1, 3, 8 })]
        public int Modificador { get; set; }

        /// <summary>
        /// Itentifica a categoria do item
        /// </summary>
        [Required]
        [ValidKey(ValidKeyAttribute.Entities.Categoria)]
        public int Categoria { get; set; }


        /// <summary>
        /// Construtor do objeto
        /// </summary>
        public AddItemViewModel()
        {

        }
    }

}
