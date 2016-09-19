using System.Net.Http;
using System.Web.Http;
using System.Net;
using SamApi.Attributes.Authorization;
using SamServices.Services;
using Swashbuckle.Swagger.Annotations;
using SamApiModels.Perfil;
using MessageSystem.Mensagem;

namespace SamApi.Controllers
{
    /// <summary>
    /// Permite obter os dados do perfil de um usuário do SAM
    /// </summary>
    [RoutePrefix("api/sam/perfil")]
    public class SamPerfilController : ApiController
    {
        /// <summary>
        /// Retorna as informações do perfil de um usuário
        /// </summary>
        /// <param name="samaccount">Identifica o usuário para qual o perfil será montado</param>
        [HttpGet]
        [Route("{samaccount}")]
        //[SwaggerResponse(HttpStatusCode.OK, "Caso seja possível obter os dados do perfil 'rh' no SAM", typeof(PerfilFuncionario))]
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível obter os dados do perfil 'funcionário' no SAM", typeof(PerfilFuncionario))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [SamResourceAuthorizer(AuthorizationType = SamResourceAuthorizer.AuthType.TokenEquality)]
        public HttpResponseMessage Get(string samaccount)
        {

            var usuario = UsuarioServices.Recupera(samaccount);
            var eventos = UsuarioServices.RecuperaEventos(usuario);
            var promocoesAdquiridas = UsuarioServices.RecuperaPromocoesAdquiridas(usuario);
            
            var perfilViewModel = new PerfilFuncionario()
            {
                Usuario = usuario,
                Atividades = eventos,
                PromocoesAdquiridas = promocoesAdquiridas
            };

            return Request.CreateResponse(HttpStatusCode.OK, perfilViewModel);


        }
    }
}
