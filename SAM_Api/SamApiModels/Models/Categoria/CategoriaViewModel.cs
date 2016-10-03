using SamModelValidationRules.Attributes.Validation;
using System.ComponentModel.DataAnnotations;

namespace SamApiModels.Categoria
{
    /// <summary>
    /// Representa a categoria de um item do SAM
    /// </summary>
    public class CategoriaViewModel
    {
        /// <summary>
        /// Identifica uma categoria no SAM
        /// </summary>
        [Required]
        [ValidKey(ValidKeyAttribute.Entities.Categoria, ErrorMessage = "Invalid value supplied to 'CategoriaViewModel.id' Check if it's a valid key")]
        public int id { get; set; }

        /// <summary>
        /// Nome da categoria do SAM
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Invalid value supplied to 'CategoriaViewModel.nome'. Size of string is greater than 50!")]
        public string nome { get; set; }

        /// <summary>
        /// Cada categoria de item, possui um peso
        /// </summary>
        [Required]
        [AllowedValues(new object[] { 3, 5, 6, 20 }, ErrorMessage = "Invalid value suplied to 'CategoriaViewModel.peso'. Valid values: (3, 5, 6, 20)")]
        public int peso { get; set; }

        /// <summary>
        /// Construtor do objeto
        /// </summary>
        public CategoriaViewModel()
        {
          
        }
     
    }
}
