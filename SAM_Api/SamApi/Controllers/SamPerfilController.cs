using System.Net.Http;
using System.Web.Http;
using System.Net;
using System.Linq;
using SamApiModels.User;
using SamApiModels.Perfil;
using SamApi.Attributes.Authorization;
using SamServices.Services;

namespace SamApi.Controllers
{

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

            var perfilViewModel = new PerfilViewModel()
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
