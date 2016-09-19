using System.Collections.Generic;
using SamApiModels.Evento;
using SamApiModels.Promocao;
using SamApiModels.User;


namespace SamApiModels.Perfil
{
    /// <summary>
    /// Representa os dados do perfil de um usuário
    /// </summary>
    public class PerfilFuncionario
    {
        /// <summary>
        /// Representa as atividades que esse funcionário fez
        /// </summary>
        public List<EventoViewModel> Atividades { get; set; }

        /// <summary>
        /// Representa as promocoes que esse funcionário adquiriu
        /// </summary>
        public List<PromocaoAdquiridaViewModel> PromocoesAdquiridas { get; set; }

        /// <summary>
        /// Representa o usuário do perfil
        /// </summary>
        public UsuarioViewModel Usuario { get; set; }

        /// <summary>
        /// Construtor do objeto
        /// </summary>
        public PerfilFuncionario()
        {

        }
    }
}
