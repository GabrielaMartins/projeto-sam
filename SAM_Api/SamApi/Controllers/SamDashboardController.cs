using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using SamApi.Attributes.Authorization;
using SamApiModels.User;
using SamServices.Services;
using Swashbuckle.Swagger.Annotations;
using SamApiModels.Dashboard;
using SamApiModels.Models.Dashboard;
using MessageSystem.Mensagem;
using System.Globalization;

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
        ///<!-- <param name="samaccount">Identifica o usuário do dashboard</param> -->
        [HttpGet]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível obter os dados do dashboard para o funcionário no SAM", typeof(DashboardFuncionario))]
        [SwaggerResponse(HttpStatusCode.OK, "Caso seja possível obter os dados do dashboard para o usuário RH no SAM", typeof(DashboardRH))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Caso a requisição não seja autorizada", typeof(DescriptionMessage))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Caso occora um erro não previsto", typeof(DescriptionMessage))]
        [SamResourceAuthorizer(Roles = "rh,funcionario")]
        public HttpResponseMessage Get()
        {

            // get perfil and samaccount from decoded token stored on request header
            var perfil = Request.Headers.GetValues("perfil").FirstOrDefault();
            var samaccountFromToken = Request.Headers.GetValues("samaccount").FirstOrDefault();
            var usuario = UsuarioServices.Recupera(samaccountFromToken);

            if (perfil.ToLower() == "rh")
            {
                var dashboard = RecuperaDashboardDoRH(usuario);
                return Request.CreateResponse(HttpStatusCode.OK, dashboard);
            }
            else
            {
                var dashboard = RecuperaDashboardDoFuncionario(usuario);
                return Request.CreateResponse(HttpStatusCode.OK, dashboard);
            }

        }

        private DashboardRH RecuperaDashboardDoRH(UsuarioViewModel usuario)
        {
            var ranking = UsuarioServices.RecuperaTodos().OrderByDescending(x => x.pontos).Take(10).ToList();

            var certicacoesMaisProcuradas = CertificacoesProcuradas();

            // ultimas atividades
            var atividades = EventoServices.RecuperaEventos(null, 10);

            // pendencias destinadas ao usuario usuario RH
            var pendencias = UsuarioServices.RecuperaPendencias(usuario);

            // proximas promocoes
            var proximasPromocoes = PromocaoServices.RecuperaProximasPromocoes();

            return new DashboardRH()
            {
                Atividades = atividades,
                CertificacoesMaisProcuradas = certicacoesMaisProcuradas,
                ProximasPromocoes = proximasPromocoes,
                Ranking = ranking,
                Pendencias = pendencias
            };
        }

        private DashboardFuncionario RecuperaDashboardDoFuncionario(UsuarioViewModel usuario)
        {
            var pendencias = UsuarioServices.RecuperaPendencias(usuario);
            var resultadoVotacoes = UsuarioServices.RecuperaVotos(usuario, 10);
            var ultimosEventos = UsuarioServices.RecuperaEventos(usuario, 10);
            var certicacoesMaisProcuradas = CertificacoesProcuradas();

            return new DashboardFuncionario
            {
                Usuario = usuario,
                ResultadoVotacoes = resultadoVotacoes,
                Atualizacoes = ultimosEventos,
                Alertas = pendencias,
                CertificacoesMaisProcuradas = certicacoesMaisProcuradas
            };
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
            int indiceCategoria = CategoriaServices.RecuperaTodas()
                .Where(
                        categoria => 
                        string.Compare(categoria.nome, "curso", CultureInfo.CurrentCulture,
                                       CompareOptions.IgnoreNonSpace |
                                       CompareOptions.IgnoreCase) == 0
                )
                .Select(y => y.id)
                .FirstOrDefault();

            var certificados = EventoServices.RecuperaEventos().Where(
                 evento => evento.Item.Categoria.id == indiceCategoria);


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