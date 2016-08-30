using SamApiModels.User;
using System.ComponentModel.DataAnnotations;

namespace SamApiModels.Promocao
{
    /// <summary>
    /// Representa uma futura promocao para um usuário
    /// </summary>
    public class ProximaPromocaoViewModel
    {
        /// <summary>
        /// Representa o funcionário que adquiriu a promoção
        /// </summary>
        [Required]
        public UsuarioViewModel Usuario { get; set; }

        /// <summary>
        /// Representa a quantidade de pontos que faltam para alcançar um novo cargo
        /// </summary>
        [Required]
        public int PontosFaltantes { get; set; }

        /// <summary>
        /// Construtor do objeto
        /// </summary>
        public ProximaPromocaoViewModel()
        {

        }
    }
}
