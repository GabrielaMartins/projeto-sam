using System.ComponentModel.DataAnnotations;

namespace SamApiModels.Cargo
{
    /// <summary>
    /// Representa um cargo do SAM
    /// </summary>
    public class CargoViewModel
    {
        /// <summary>
        /// Identifica o cargo no sistema do SAM
        /// </summary>
        [Required]
        public int id { get; set; }

        /// <summary>
        /// Nome do cargo
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "Size of string is greater than 50!")]
        public string nome { get; set; }

        /// <summary>
        /// É o número de pontos exigidos para atingir o cargo
        /// </summary>
        [Required]
        public int pontuacao { get; set; }

        /// <summary>
        /// Construtor do objeto
        /// </summary>
        public CargoViewModel()
        {
            
        }

    }
}
