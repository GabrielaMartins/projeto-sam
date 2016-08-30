using System.Web.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Web.Http.Description;
using SamHelpers;

namespace SamApiService.Controllers
{

    [RoutePrefix("api/sam/ad")]
    public class ActiveDirectoryController : ApiController
    {

        // GET api/ad/user/all
        [Route("user/all")]
        [HttpGet]
        [ResponseType(typeof(List<ActiveDirectoryUser>))]
        public HttpResponseMessage GetUsers()
        {

            var adConsumer = new ActiveDirectoryHelper("opus.local");
            var users = adConsumer.GetAllUsers();

            return Request.CreateResponse(HttpStatusCode.OK, users);
          
        }

        // GET api/ad/User/{samaccount}
        [Route("user/{samaccount}")]
        [HttpGet]
        [ResponseType(typeof(ActiveDirectoryUser))]
        public HttpResponseMessage GetUser(string samAccount)
        {

            var adConsumer = new ActiveDirectoryHelper("opus.local");
            var user = adConsumer.GetUser(samAccount);

            return Request.CreateResponse(HttpStatusCode.OK, user);
        }
    }

}