using SamModelValidationRules.Attributes.Validation;
using System.ComponentModel.DataAnnotations;

namespace SamApiModels.Item
{
    /// <summary>
    /// Representa os dados para alterar um item
    /// </summary>
    public class UpdateItemViewModel
    {
        /// <summary>
        /// Identifica o item a ser alterado
        /// </summary>
        [Required]
        [ValidKey(ValidKeyAttribute.Entities.Item, ErrorMessage = "Invalid value supplied to 'UpdateItemViewModel.Id'. Check if it's a valid key")]
        public int Id { get; set; }

        /// <summary>
        /// Identifica a categoria associada ao item
        /// </summary>
        [Required]
        [ValidKey(ValidKeyAttribute.Entities.Categoria, ErrorMessage = "Invalid value supplied to 'UpdateItemViewModel.Categoria'. Check if it's a valid key")]
        public int Categoria { get; set; }

        /// <summary>
        /// É o valor da dificuldade do item
        /// </summary>
        [Required]
        [AllowedValues(new object[] { 1, 3, 8 }, ErrorMessage = "Invalid value supplied to 'UpdateItemViewModel.Dificuldade'. Valid values: (1, 3, 8)")]
        public int Dificuldade { get; set; }

        /// <summary>
        /// É o peso dado ao item
        /// </summary>
        [Required]
        [AllowedValues(new object[] { 1, 2, 3, 8 }, ErrorMessage = "Invalid value supplied to 'UpdateItemViewModel.Modificador'. Valid values: (1, 2, 3, 8)")]
        public int Modificador { get; set; }

        /// <summary>
        /// Representa o nome do item
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Invalid value supplied to 'UpdateItemViewModel.Nome'. String size is greater than 50 characters")]
        public string Nome { get; set; }

        /// <summary>
        /// Representa a descrição do item
        /// </summary>
        [Required]
        [StringLength(200, ErrorMessage = "Invalid value supplied to 'UpdateItemViewModel.Descricao'. String size is greater than 200 characters")]
        public string Descricao { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public UpdateItemViewModel()
        {

        }
    }
}
