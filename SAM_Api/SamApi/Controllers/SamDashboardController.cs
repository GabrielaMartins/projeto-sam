using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using SamApi.Attributes.Authorization;
using SamApiModels.User;
using SamServices.Services;
using Swashbuckle.Swagger.Annotations;
using DefaultException.Models;
using SamApiModels.Dashboard;

namespace SamApi.Controllers
{
    /// <summary>
    /// Permite obter os dados do dashboard de um usuário do SAM
    /// </summary>
    [RoutePrefix("api/sam/Dashboard")]
    public class SamDashboardController : ApiController
    {
        /// <summary>
        /// Recupera as informações do dashboard do funcionário
        /// </summary>
        /// <param name="samaccount">Identifica o usuário do dashboard</param>
        [HttpGet]
        [Route("{samaccount}")]
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível obter os dados do dashboard do usuário do SAM", typeof(Dashboard))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [SamResourceAuthorizer(AuthorizationType = SamResourceAuthorizer.AuthType.TokenEquality)]
        public HttpResponseMessage Get(string samaccount)
        {

            // get perfil from decoded token stored on request header
            var perfil = Request.Headers.GetValues("perfil").FirstOrDefault();
            var usuario = UserServices.Recupera(samaccount);

            if (perfil == "rh")
            {
                // coisas do rh aqui
                var ranking = Ranking();
                var certicacoesMaisProcuradas = CertificacoesProcuradas();

                return Request.CreateResponse(HttpStatusCode.OK, "not implemented");
            }
            else
            {
                var pendencias = UserServices.RecuperaPendencias(usuario);
                var resultadoVotacoes = UserServices.RecuperaVotos(usuario, 10);
                var ultimosEventos = UserServices.RecuperaEventos(usuario, 10);
                var certicacoesMaisProcuradas = CertificacoesProcuradas();

                var dashFuncionario = new Dashboard
                {
                    Usuario = usuario,
                    ResultadoVotacoes = resultadoVotacoes,
                    Atualizacoes = ultimosEventos,
                    Alertas = pendencias,
                    CertificacoesMaisProcuradas = certicacoesMaisProcuradas
                };

                return Request.CreateResponse(HttpStatusCode.OK, dashFuncionario);
            }

        }


        private List<UsuarioViewModel> Ranking()
        {

            List<UsuarioViewModel> ranking = UserServices.RecuperaTodos().OrderByDescending(x => x.pontos).Take(10).ToList();

            var rankingViewModel = new List<UsuarioViewModel>();
            foreach (var usuario in ranking)
            {
                var usuarioViewModel = usuario;
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

            //Consulta para encontrar itens que são certificados em eventos
            int indiceCategoria = CategoriaServices.RecuperaTodas().Where(
                        categoria => categoria.nome == "Certificação"
                    ).Select(y => y.id).First();

            var certificados = EventoServices.RecuperaEventos().Where(
                 evento => evento.Item.Categoria.id.Equals(indiceCategoria));


            //Obtém categorias e cria as colunas
            List<string> nomesCertificados = certificados.Select(x => x.Item.nome).Distinct().ToList();


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