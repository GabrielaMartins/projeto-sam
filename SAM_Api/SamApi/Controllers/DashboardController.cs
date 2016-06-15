using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Opus.DataBaseEnvironment;
using SamApiModels;
using System.Net.Http.Headers;
using System.Collections.Generic;

namespace SamApi.Controllers
{
    [RoutePrefix("api/sam/Dashboard")]
    public class DashboardController : ApiController
    {
        // GET: api/sam/Dashboard/ultimosEventos
        [Route("ultimosEventos")]
        [HttpGet]
        public HttpResponseMessage UltimosEventos()
        {

            var eventosRepository = DataAccess.Instance.EventoRepository();

            var ultimosEventos = eventosRepository.GetAll().OrderByDescending(x => x.data).ThenBy(x => x.Item.nome).Take(10).Select(x => new UltimosEventos
            {
                nome = x.Usuario.nome,
                data = x.data.ToString(),
                idEvento = x.id,
                evento = x.Item.nome,
                imagem = x.Usuario.foto
            }).ToList();

            var response = Request.CreateResponse(HttpStatusCode.OK, ultimosEventos);
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };

            return response;
        }

        // GET: api/sam/Dashboard/proximasPromocoes
        [Route("proximasPromocoes")]
        [HttpGet]
        public HttpResponseMessage ProximasPromocoes()
        {
            var usuarioRepositorio = DataAccess.Instance.UsuarioRepository();

            var proximasPromocoes = usuarioRepositorio.proximasPromocoes();

            var response = Request.CreateResponse(HttpStatusCode.OK, proximasPromocoes);
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };

            return response;


        }

        // POST: api/sam/Dashboard/pendencias
        /*[Route("pendencias")]
        [HttpPost]
        public HttpResponseMessage Pendencias(int id)
        {
            var usuarioRepositorio = DataAccess.Instance.UsuarioRepository();

            List<PendenciaViewModel> pendencias = usuarioRepositorio.GetAll().First(x => x.id == id).Pendencias.Select( x => new PendenciaViewModel {
                    Continua
            });

            var response = Request.CreateResponse(HttpStatusCode.OK, pendencias);
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };

            return response;


        }*/

        // POST: api/sam/Dashboard/ranking
        [Route("ranking")]
        [HttpGet]
        public HttpResponseMessage Ranking()
        {
            var usuarioRepositorio = DataAccess.Instance.UsuarioRepository();

            List<dynamic> ranking = usuarioRepositorio.GetAll().OrderByDescending(x => x.pontos).Take(10).Select(x => new {
                nome = x.nome,
                imagem = x.foto,
                pontos = x.pontos
            }).ToList<dynamic>();

            var response = Request.CreateResponse(HttpStatusCode.OK, ranking);
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };

            return response;


        }


    }
}
