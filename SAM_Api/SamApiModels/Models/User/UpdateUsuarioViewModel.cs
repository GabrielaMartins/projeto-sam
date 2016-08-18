using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamApiModels.Models.User
{
    public class UpdateUsuarioViewModel
    {

     
        [Required]
        public string nome { get; set; }

        [Required]
        public DateTime dataInicio { get; set; }

        [Required]
        public int pontos { get; set; }

        [Required]
        public string descricao { get; set; }

        [Required]
        public string facebook { get; set; }

        public string github { get; set; }

        public string linkedin { get; set; }

        [Required]
        public string foto { get; set; }

        [Required]
        public string perfil { get; set; }

        [Required]
        public int cargo { get; set; }

        public UpdateUsuarioViewModel()
        {

        }
    }
}
