using System.Web.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using SamApiModels.Categoria;
using Swashbuckle.Swagger.Annotations;
using SamApi.Attributes.Authorization;
using SamServices.Services;
using MessageSystem.Mensagem;

namespace SamApi.Controllers
{
    /// <summary>
    /// Permite efetuar ações sobre as categorias do SAM
    /// </summary>
    [RoutePrefix("api/sam/category")]
    public class SamCategoriaController : ApiController
    {
        /// <summary>
        /// Retorna a lista de categorias do SAM
        /// </summary>
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível obter a lista de categorias do SAM", typeof(List<CategoriaViewModel>))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [SamResourceAuthorizer(Roles = "rh,funcionario")]
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage Get()
        {
            var categorias = CategoriaServices.RecuperaTodas();
            return Request.CreateResponse(HttpStatusCode.OK, categorias);
            
        }

        /// <summary>
        /// Retorna a lista de categorias do SAM
        /// </summary>
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível obter a lista de categorias do SAM", typeof(List<CategoriaViewModel>))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [SamResourceAuthorizer(Roles = "rh,funcionario")]
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage Get(int id)
        {

            var categoria = CategoriaServices.Recupera(id);
            return Request.CreateResponse(HttpStatusCode.OK, categoria);
        }
    }
}
