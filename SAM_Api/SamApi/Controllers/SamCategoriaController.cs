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

namespace SamApi.Controllers
{
    [RoutePrefix("api/sam/category")]
    public class SamCategoriaController : ApiController
    {
        // GET: api/sam/categoria/all
        [Route("all")]
        public HttpResponseMessage Get()
        {
            var categorias = DataAccess.Instance.CategoriaRepository().GetAll().ToList();
            var categoriasViewModel = new List<CategoriaViewModel>();
            foreach(var categoria in categorias)
            {
                var categoriaViewModel = Mapper.Map<Categoria, CategoriaViewModel>(categoria);
                categoriasViewModel.Add(categoriaViewModel);
            }

            return Request.CreateResponse(HttpStatusCode.OK, categoriasViewModel);
        }

        // GET: api/sam/categoria/{id}
        [Route("{id}")]
        public HttpResponseMessage Get(int id)
        {

            // erase here
            var response = Request.CreateResponse(HttpStatusCode.OK, new MessageViewModel(HttpStatusCode.ServiceUnavailable, "Not Implemented", "under construction"));
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };

            return response;
        }

    }
}
