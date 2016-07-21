using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Opus.DataBaseEnvironment;
using SamApiModels;
using System.Collections.Generic;
using SamDataBase.Model;
using AutoMapper;
using Opus.Helpers;

namespace SamApi.Controllers
{
    [RoutePrefix("api/sam/Dashboard")]
    public class SamDashboardController : ApiController
    {

        // GET: api/sam/Dashboard
        [HttpGet]
        [Route("")]
        public HttpResponseMessage Get()
        {

            var token = HeaderHandler.ExtractHeaderValue(Request, "token");
            if (token == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, MessageViewModel.TokenMissing);
            }

            var decodedToken = JwtManagement.DecodeToken(token.SingleOrDefault());
            var context = decodedToken["context"] as Dictionary<string, object>;
            var userInfo = context["user"] as Dictionary<string, object>;
            var samaccount = userInfo["samaccount"] as string;
            var perfil = context["perfil"] as string;

            using (var userRep = DataAccess.Instance.GetUsuarioRepository())
            {
                var usuario = userRep.Find(u => u.samaccount == samaccount).SingleOrDefault();

                var ultimosEventos = UltimosEventos();
                var ranking = Ranking();
                var certificacoes = CertificacoesProcuradas();

                var proximasPromocoes = new List<ProximaPromocaoViewModel>();
                if (perfil == "RH")
                {
                    proximasPromocoes = ProximasPromocoes();
                }
                else
                {
                    proximasPromocoes = ProximasPromocoes(usuario);
                }

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
        }

        private List<UltimoEventoViewModel> UltimosEventos()
        {

            using (var eventosRepository = DataAccess.Instance.GetEventoRepository())
            {

                // TODO: refatorar isso depois, tentar inserir como um metodo do repositorio de eventos
                var ultimosEventos = eventosRepository.GetAll()
                    .OrderByDescending(x => x.data)
                    .ThenBy(x => x.Item.nome)
                    .Take(10).AsEnumerable()
                    .Select(x =>
                        new UltimoEventoViewModel
                        {
                            Evento = Mapper.Map<Evento, EventoViewModel>(x),
                            UsuariosQueFizeram = DataAccess.Instance.GetItemRepository().RecuperaUsuariosQueFizeram(x.item.Value)
                        }).ToList();

                return ultimosEventos;
            }
        }

        private List<ProximaPromocaoViewModel> ProximasPromocoes(Usuario usuario = null)
        {
            List<ProximaPromocaoViewModel> proximasPromocoes = new List<ProximaPromocaoViewModel>();
            if (usuario != null)
            {
                // Dashboard for normal staff
                using (var userRep = DataAccess.Instance.GetUsuarioRepository())
                {
                    proximasPromocoes = userRep.RecuperaProximasPromocoes(usuario);
                }
            }
            else
            {
                // Dashboard for HR(human resources) staff
                using (var promoRep = DataAccess.Instance.GetPromocaoRepository())
                {
                    proximasPromocoes = promoRep.RecuperaProximasPromocoes();
                }
            }

            return proximasPromocoes;
        }

        private List<UsuarioViewModel> Ranking()
        {
            using (var userRep = DataAccess.Instance.GetUsuarioRepository())
            {

                List<Usuario> ranking = userRep.GetAll().OrderByDescending(x => x.pontos).Take(10).ToList();
                var rankingViewModel = new List<UsuarioViewModel>();
                foreach (var usuario in ranking)
                {
                    var usuarioViewModel = Mapper.Map<Usuario, UsuarioViewModel>(usuario);
                    rankingViewModel.Add(usuarioViewModel);
                }

                return rankingViewModel;
            }
        }

        // TODO: REFATORAR ESSE MÉTODO
        private List<dynamic> CertificacoesProcuradas()
        {
            List<dynamic> colunas = new List<dynamic>();
            List<dynamic> linha = new List<dynamic>();
            List<dynamic> tabela = new List<dynamic>();

            var opGrafico = new Dictionary<string, string>();
            opGrafico.Add("role", "annotation");

            using (var eventoRepositorio = DataAccess.Instance.GetEventoRepository())
            using (var categoriaRepositorio = DataAccess.Instance.GetCategoriaRepository())
            {

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
}