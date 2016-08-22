using AutoMapper;
using DefaultException.Models;
using Opus.DataBaseEnvironment;
using SamApiModels.Pendencia;
using SamDataBase.Model;
using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SamApi.Controllers
{
    /// <summary>
    /// Controla as ações referentes ás pendências do SAM
    /// </summary>
    [RoutePrefix("api/sam/pendency")]
    public class SamPendencyController : ApiController
    {

        /// <summary>
        /// Recupera uma pendência específica do sam
        /// </summary>
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível obter a pendência do SAM", typeof(PendenciaEventoViewModel))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [HttpGet]
        [Route("{pendency}")]
        public HttpResponseMessage Get(int pendency)
        {
            using(var rep = DataAccess.Instance.GetPendenciaRepository())
            {
                var pendencia = rep.Find(p => p.id == pendency).SingleOrDefault();

                var pendenciaViewModel = Mapper.Map<Pendencia, PendenciaEventoViewModel>(pendencia);

                return Request.CreateResponse(HttpStatusCode.OK, pendenciaViewModel);
            }
        }

        /// <summary>
        /// Recupera a lista de pendências do sam
        /// </summary>
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível obter a lista de pendências do SAM", typeof(PendenciaEventoViewModel))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage GetAll()
        {
            using (var rep = DataAccess.Instance.GetPendenciaRepository())
            {
                var pendencias = rep.GetAll().ToList();

                var pendenciasViewModel = Mapper.Map<List<Pendencia>, List<PendenciaEventoViewModel>>(pendencias);

                return Request.CreateResponse(HttpStatusCode.OK, pendenciasViewModel);
            }
        }

        /// <summary>
        /// Remove uma pendência específica do sam
        /// </summary>
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível remover a pendência do SAM", typeof(PendenciaEventoViewModel))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [HttpDelete]
        [Route("delete/{pendency}")]
        public HttpResponseMessage Delete(int pendency)
        {
            using (var rep = DataAccess.Instance.GetPendenciaRepository())
            {

                rep.Delete(pendency);

                return Request.CreateResponse(HttpStatusCode.OK, new DescriptionMessage(HttpStatusCode.OK, "Pendency removed"));
            }
        }

    }
}
