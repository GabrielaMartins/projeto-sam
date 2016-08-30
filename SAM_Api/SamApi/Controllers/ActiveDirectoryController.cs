using System.Web.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Web.Http.Description;
using SamHelpers;
using Swashbuckle.Swagger.Annotations;
using SamApi.Attributes.Authorization;
using DefaultException.Models;

namespace SamApiService.Controllers
{

    /// <summary>
    /// Recupera informações do AD da OPUS
    /// </summary>
    [RoutePrefix("api/sam/ad")]
    public class ActiveDirectoryController : ApiController
    {
        /// <summary>
        /// Retorna a lista dos usuários do AD da Opus
        /// </summary>
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível obter a lista de usuários do SAM", typeof(List<ActiveDirectoryUser>))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [SamResourceAuthorizer(Roles = "rh")]
        [Route("user/all")]
        [HttpGet]
        public HttpResponseMessage GetUsers()
        {

            var adConsumer = new ActiveDirectoryHelper("opus.local");
            var users = adConsumer.GetAllUsers();

            return Request.CreateResponse(HttpStatusCode.OK, users);
          
        }

        /// <summary>
        /// Retorna o usuário do AD da Opus
        /// </summary>
        /// <param name="samaccount"></param>

        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível o usuário do AD da Opus", typeof(ActiveDirectoryUser))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [SamResourceAuthorizer(Roles = "rh")]
        [Route("user/{samaccount}")]
        [HttpGet]
        public HttpResponseMessage GetUser(string samaccount)
        {

            var adConsumer = new ActiveDirectoryHelper("opus.local");
            var user = adConsumer.GetUser(samaccount);

            return Request.CreateResponse(HttpStatusCode.OK, user);
        }
    }

}