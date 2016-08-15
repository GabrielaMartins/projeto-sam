using System.Web.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System;
using SamApiModels;
using Opus.DataBaseEnvironment;
using System.Linq;
using AutoMapper;
using SamDataBase.Model;
using SamApiModels.Categoria;
using Swashbuckle.Swagger.Annotations;
using DefaultException.Models;
using SamApi.Attributes;

namespace SamApi.Controllers
{
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
           
            using (var rep = DataAccess.Instance.GetCategoriaRepository()) {
                var categorias = rep.GetAll().ToList();
                var categoriasViewModel = new List<CategoriaViewModel>();
                foreach (var categoria in categorias)
                {
                    var categoriaViewModel = Mapper.Map<Categoria, CategoriaViewModel>(categoria);
                    categoriasViewModel.Add(categoriaViewModel);
                }

                return Request.CreateResponse(HttpStatusCode.OK, categoriasViewModel);
            }
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
            using (var rep = DataAccess.Instance.GetCategoriaRepository())
            {
                var query = rep.Find(c => c.id == id);
                var categoria = query.SingleOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, categoria);
            }  
        }

    }
}
