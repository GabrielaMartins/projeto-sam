using System.Net.Http;
using System.Web.Http;
using System.Net;
using SamApi.Attributes.Authorization;
using SamServices.Services;

namespace SamApi.Controllers
{
    /// <summary>
    /// Oferece ações referentes ao perfil de um usuário
    /// </summary>
    [RoutePrefix("api/sam/perfil")]
    public class SamPerfilController : ApiController
    {

        [HttpGet]
        [Route("{samaccount}")]
        [SamResourceAuthorizer(AuthorizationType = SamResourceAuthorizer.AuthType.TokenEquality)]
        public HttpResponseMessage Get(string samaccount)
        {

            var usuario = UserServices.Recupera(samaccount);
            var eventos = UserServices.RecuperaEventos(usuario);
            var promocoesAdquiridas = UserServices.RecuperaPromocoesAdquiridas(usuario);

            var perfilViewModel = new
            {
                Usuario = usuario,
                Atividades = eventos,
                PromocoesAdquiridas = promocoesAdquiridas
            };

            return Request.CreateResponse(HttpStatusCode.OK, perfilViewModel);


        }

        private void CreateChart()
        {

        }
    }
}
