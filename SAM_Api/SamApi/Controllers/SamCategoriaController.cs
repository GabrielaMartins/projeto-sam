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

namespace SamApi.Controllers
{
    [RoutePrefix("api/sam/category")]
    public class SamCategoriaController : ApiController
    {
        // GET: api/sam/category/all
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

        // GET: api/sam/categoria/{id}
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
