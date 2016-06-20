using System.Net.Http;
using System.Web.Http;
using System.Net;
using Opus.DataBaseEnvironment;
using SamApiModels;
using System.Linq;
using AutoMapper;
using SamDataBase.Model;

namespace SamApi.Controllers
{

    [RoutePrefix("api/sam/perfil")]
    public class PerfilController : ApiController
    {

        [Route("{samaccount}")]
        [HttpGet]
        public HttpResponseMessage Get(string samaccount)
        {

            // fazer verificações com o token
            //CommonOperations commonOperations = new CommonOperations(Request);
            //HttpResponseMessage response = null;

            //// this line check and prepare some variables for us
            //commonOperations.Check();

            //// if we have response, so it's an error
            //if (commonOperations.ResponseError != null)
            //    return commonOperations.ResponseError;

            //var token = commonOperations.DecodedToken;


            var perfil = DataAccess.Instance.UsuarioRepository().RecuperaPerfil(samaccount);
            if (perfil == null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new MessageViewModel(HttpStatusCode.NotFound, "Perfil not found", "we can't find perfil for this user"));
            }

            return Request.CreateResponse(HttpStatusCode.OK, perfil);

        }

    }
}
