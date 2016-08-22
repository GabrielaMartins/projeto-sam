﻿using AutoMapper;
using DefaultException.Models;
using Opus.DataBaseEnvironment;
using SamApi.Attributes.Authorization;
using SamApiModels.Cargo;
using SamDataBase.Model;
using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SamApi.Controllers
{
    [RoutePrefix("api/sam/role")]
    public class SamCargoController : ApiController
    {
        /// <summary>
        /// Retorna a lista de cargos do SAM
        /// </summary>
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível obter a lista de cargos do SAM", typeof(List<CargoViewModel>))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [Route("all")]
        [HttpGet]
        [SamResourceAuthorizer(Roles = "rh,funcionario")]
        public HttpResponseMessage GetAll()
        {
            using (var rep = DataAccess.Instance.GetCargoRepository())
            {
                var cargos = Mapper.Map<List<Cargo>,List<CargoViewModel>>(rep.GetAll().ToList());
                return Request.CreateResponse(HttpStatusCode.OK, cargos);
            }
        }
    }
}