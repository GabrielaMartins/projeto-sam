using SamApiModels.Evento;
using SamApiModels.Promocao;
using SamApiModels.User;
using System.Collections.Generic;

namespace SamApiModels.Models.Dashboard
{
    /// <summary>
    /// Representa as informações do dashboard do RH
    /// </summary>
    public class DashboardRH
    {
        /// <summary>
        /// COMENTAR
        /// </summary>
        public List<UsuarioViewModel> Ranking { get; set; }

        /// <summary>
        /// COMENTAR
        /// </summary>
        public List<dynamic> CertificacoesMaisProcuradas { get; set; }

        /// <summary>
        /// COMENTAR
        /// </summary>
        public List<EventoViewModel> Atividades { get; set; }

        /// <summary>
        /// COMENTAR
        /// </summary>
        public List<ProximaPromocaoViewModel> ProximasPromocoes { get; set; }

        /// <summary>
        /// Construtor do objeto
        /// </summary>
        public DashboardRH()
        {

        }
    }
}
