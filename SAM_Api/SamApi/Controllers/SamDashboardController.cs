using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Opus.DataBaseEnvironment;
using System.Collections.Generic;
using SamDataBase.Model;
using AutoMapper;
using SamApi.Attributes;
using SamApiModels.User;

namespace SamApi.Controllers
{
    [RoutePrefix("api/sam/Dashboard")]
    public class SamDashboardController : ApiController
    {

        // GET: api/sam/Dashboard
        [HttpGet]
        [Route("{samaccount}")]
        [SamResourceAuthorizer(AuthorizationType = SamResourceAuthorizer.AuthType.TokenEquality)]
        public HttpResponseMessage Get(string samaccount)
        {

            // get perfil from decoded token stored on request header
            var perfil = Request.Headers.GetValues("perfil").FirstOrDefault();
            
            using (var userRep = DataAccess.Instance.GetUsuarioRepository())
            {
                
                var usuario = userRep.Find(u => u.samaccount == samaccount).SingleOrDefault();
 
                if (perfil == "RH")
                {
                    // coisas do rh aqui
                    var ranking = Ranking();
                    var certicacoesMaisProcuradas = CertificacoesProcuradas();

                    return Request.CreateResponse(HttpStatusCode.OK, "not implemented");
                }
                else
                {
                    var pendencias = userRep.RecuperaPendencias(usuario);
                    var resultadoVotacoes = userRep.RecuperaVotacoes(usuario);
                    var ultimosEventos = userRep.RecuperaEventos(usuario, 10);
                    var certicacoesMaisProcuradas = CertificacoesProcuradas();
                    var usuarioViewModel = Mapper.Map<Usuario, UsuarioViewModel>(usuario);

                    var dashFuncionario = new
                    {
                        Usuario = usuarioViewModel,
                        ResultadoVotacoes = resultadoVotacoes,
                        Atualizacoes = ultimosEventos,
                        Alertas = pendencias,
                        CertificacoesMaisProcuradas = certicacoesMaisProcuradas
                    };

                    return Request.CreateResponse(HttpStatusCode.OK, dashFuncionario);
                }
            }
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