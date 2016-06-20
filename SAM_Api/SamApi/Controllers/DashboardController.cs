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
                data = x.data,
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

        // GET: api/sam/Dashboard/ranking
        [Route("ranking")]
        [HttpGet]
        public HttpResponseMessage Ranking()
        {
            var usuarioRepositorio = DataAccess.Instance.UsuarioRepository();

            var ranking = usuarioRepositorio.GetAll().OrderByDescending(x => x.pontos).Take(10).Select(x => new UsuarioViewModel{
                nome = x.nome,
                imagem = x.foto,
                pontos = x.pontos
            }).ToList();

            var response = Request.CreateResponse(HttpStatusCode.OK, ranking);
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };

            return response;


        }

        // GET: api/sam/Dashboard/ranking
        [Route("certificacoesProcuradas")]
        [HttpGet]
        public HttpResponseMessage CertificacoesProcuradas()
        {
            List<dynamic> colunas = new List<dynamic>();
            List<dynamic> linha = new List<dynamic>();
            List<dynamic> tabela = new List<dynamic>();

            var opGrafico = new Dictionary<string, string>();
            opGrafico.Add("role", "annotation");

           
            var eventoRepositorio = DataAccess.Instance.EventoRepository();
            var categoriaRepositorio = DataAccess.Instance.CategoriaRepository();


            //Consulta para encontrar itens que são certificados em eventos
            int indiceCategoria = categoriaRepositorio.GetAll().Where(
                        categoria => categoria.nome == "Certificação"
                    ).Select(y => y.id).First();

            var certificados = eventoRepositorio.GetAll().Where(
                 evento => evento.Item.Categoria.id.Equals(indiceCategoria));


            //Obtém categorias e cria as colunas
            List<String> nomesCertificados = certificados.Select(x => x.Item.nome).Distinct().ToList();


            colunas.Add("Certificações");
            
            foreach (var certificado in nomesCertificados)
            {
                colunas.Add(certificado);
            }

            colunas.Add(opGrafico);

            tabela.Add(colunas);

            //Verifica para cada certificação quantas vezes ao longo de um ano ela foi procurada

            var anos = certificados.Select(certificado => certificado.data.Year).Distinct().ToList();

            foreach (var ano in anos)
            {
                linha.Add(ano.ToString());

                foreach(var certificado in nomesCertificados)
                {
                    var quantidadesCertificacoes = certificados.Where(x => x.Item.nome == certificado && x.data.Year == ano).GroupBy(x => x.Item.nome).Count();
                    linha.Add(quantidadesCertificacoes);
                }
                linha.Add("");
                tabela.Add(linha);
                linha = new List<dynamic>();
            }

           
            

            var response = Request.CreateResponse(HttpStatusCode.OK, tabela);
             response.Headers.CacheControl = new CacheControlHeaderValue()
             {
                 MaxAge = TimeSpan.FromMinutes(20)
             };
            
            return response;


        }


    }
}

