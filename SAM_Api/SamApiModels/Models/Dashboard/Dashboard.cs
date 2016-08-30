using SamApiModels.Evento;
using SamApiModels.User;
using SamApiModels.Votacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamApiModels.Dashboard
{
    /// <summary>
    /// Representa os dados do dashboard do usuário
    /// </summary>
    public class Dashboard
    {
        /// <summary>
        /// Representa o usuário do dashboard
        /// </summary>
        public UsuarioViewModel Usuario { get; set; }

        /// <summary>
        /// Representa as votações que esse usuário fez
        /// </summary>
        public List<VotoViewModel> ResultadoVotacoes { get; set; }

        /// <summary>
        /// Representa as atualizações referentes a esse usuário
        /// </summary>
        public List<EventoViewModel> Atualizacoes { get; set; }

        /// <summary>
        /// Representa os alertas referentes a esse usuário
        /// </summary>
        public List<PendenciaUsuarioViewModel> Alertas { get; set; }

        /// <summary>
        /// Representa os dados do gráfico para o dashboard
        /// </summary>
        public List<dynamic> CertificacoesMaisProcuradas { get; set; }

        /// <summary>
        /// Construtor do objeto
        /// </summary>
        public Dashboard()
        {

        }
    }
}
