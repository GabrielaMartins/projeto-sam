using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SamApi.Controllers
{
    [RoutePrefix("api/sam")]
    public class HomeController : Controller
    {
        [Route("home")]
        public ActionResult Index()
        {
            return View();
        }
    }
}