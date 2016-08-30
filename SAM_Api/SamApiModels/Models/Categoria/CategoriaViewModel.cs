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
        [ValidKey(ValidKeyAttribute.Entities.Categoria)]
        public int id { get; set; }

        /// <summary>
        /// Nome da categoria do SAM
        /// </summary>
        [Required]
        public string nome { get; set; }

        /// <summary>
        /// Cada categoria de item, possui um peso
        /// </summary>
        [Required]
        [AllowedValues(new[] {3, 5, 6, 20})]
        public int peso { get; set; }

        /// <summary>
        /// Construtor do objeto
        /// </summary>
        public CategoriaViewModel()
        {
          
        }
     
    }
}
