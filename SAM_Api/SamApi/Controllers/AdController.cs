using System.Web.Http;
using System.Collections.Generic;
using Opus.Helpers.ActiveDirectoryService;
using SamApiModels;
using System.Net.Http;
using System.Net;

namespace SamApiService.Controllers
{

    [RoutePrefix("api/sam/ad")]
    public class AdController : ApiController
    {

        // GET api/ad/user/all
        [Route("user/all")]
        [HttpGet]
        public HttpResponseMessage GetUsers()
        {

            var adConsumer = new ActiveDirectoryHelper("opus.local");
            var users = adConsumer.GetAllUsers();

            return Request.CreateResponse(HttpStatusCode.OK, users);
          
        }

        // GET api/ad/User/{samaccount}
        [Route("user/{samaccount}")]
        [HttpGet]
        public HttpResponseMessage GetUser(string samAccount)
        {

            var adConsumer = new ActiveDirectoryHelper("opus.local");
            var user = adConsumer.GetUser(samAccount);

            return Request.CreateResponse(HttpStatusCode.OK, user);
        }
    }

}