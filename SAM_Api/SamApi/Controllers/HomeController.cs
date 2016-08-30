using System.Web.Mvc;

namespace SamApi.Controllers
{
    /// <summary>
    /// Mostra a pagina inicial da API
    /// </summary>
    [RoutePrefix("api/sam")]
    public class HomeController : Controller
    {
        /// <summary>
        /// Exibe a página inicial da API
        /// </summary>
        /// <returns>Retorna a página inicial da API</returns>
        [Route("home")]
        public ActionResult Index()
        {
            return View();
        }
    }
}