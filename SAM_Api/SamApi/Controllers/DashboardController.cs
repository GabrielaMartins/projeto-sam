using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Opus.DataBaseEnvironment;
using SamApiModels;
using System.Net.Http.Headers;
using System.Collections.Generic;
using SamDataBase.Model;
using AutoMapper;

namespace SamApi.Controllers
{
    [RoutePrefix("api/sam/Dashboard")]
    public class DashboardController : ApiController
    {

        // GET: api/sam/Dashboard/samaccount
        [Route("{samaccount}")]
        [HttpGet]
        public HttpResponseMessage Get(string samaccount)
        {
            var usuario = DataAccess.Instance.UsuarioRepository().Find(u => u.samaccount == samaccount).SingleOrDefault();

            var ultimosEventos = UltimosEventos();
            var proximasPromocoes = ProximasPromocoes(usuario);
            var ranking = Ranking();
            var certificacoes = CertificacoesProcuradas();

            var dashboardViewModel = new DashboardViewModel()
            {
                Usuario = Mapper.Map<Usuario, UsuarioViewModel>(usuario),
                UltimosEventos = ultimosEventos,
                ProximasPromocoes = proximasPromocoes,
                Ranking = ranking,
                CertificacoesMaisProcuradas = certificacoes
            };

            return Request.CreateResponse(HttpStatusCode.OK, dashboardViewModel);
        }

        private List<UltimoEventoViewModel> UltimosEventos()
        {

            var eventosRepository = DataAccess.Instance.EventoRepository();

            // TODO: refatorar isso depois, tentar inserir como um metodo do repositorio de eventos
            var ultimosEventos = eventosRepository.GetAll()
                .OrderByDescending(x => x.data)
                .ThenBy(x => x.Item.nome)
                .Take(10).AsEnumerable()
                .Select(x =>
                    new UltimoEventoViewModel
                    {
                        Evento = Mapper.Map<Evento, EventoViewModel>(x),
                        UsuariosQueFizeram = DataAccess.Instance.ItemRepository().RecuperaUsuariosQueFizeram(x.item.Value)
                    }).ToList();
            
            return ultimosEventos;
        }

        private List<PromocaoViewModel> ProximasPromocoes(Usuario usuario)
        {
            var proximasPromocoes = DataAccess.Instance.UsuarioRepository().RecuperaProximasPromocoes(usuario);
            return proximasPromocoes;
        }

        private List<UsuarioViewModel> Ranking()
        {
            var usuarioRepositorio = DataAccess.Instance.UsuarioRepository();

            List<Usuario> ranking = usuarioRepositorio.GetAll().OrderByDescending(x => x.pontos).Take(10).ToList();
            var rankingViewModel = new List<UsuarioViewModel>();
            foreach(var usuario in ranking)
            {
                var usuarioViewModel = Mapper.Map<Usuario, UsuarioViewModel>(usuario);
                rankingViewModel.Add(usuarioViewModel);
            }

            return rankingViewModel;

        }

        // TODO: REFATORAR ESSE MÉTODO
        private List<dynamic> CertificacoesProcuradas()
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

                foreach (var certificado in nomesCertificados)
                {
                    var quantidadesCertificacoes = certificados.Where(x => x.Item.nome == certificado && x.data.Year == ano).GroupBy(x => x.Item.nome).Count();
                    linha.Add(quantidadesCertificacoes);
                }
                linha.Add("");
                tabela.Add(linha);
                linha = new List<dynamic>();
            }


            return tabela;

        }

    }
}
