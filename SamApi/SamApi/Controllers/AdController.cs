using System.Web.Http;
using System.Collections.Generic;
using Opus.Helpers.ActiveDirectoryService;

namespace SamApiService.Controllers
{

    [RoutePrefix("api/sam/ad")]
    public class AdController : ApiController
    {

        // GET api/ad/user/all
        [Route("user/all")]
        [HttpGet]
        public List<ActiveDirectoryUser> GetUsers()
        {

            var adConsumer = new ActiveDirectoryConsumer("opus.local");
            var users = adConsumer.GetAllUsers();

            return users;
        }

        // GET api/ad/User/{samaccount}
        [Route("user/{samaccount}")]
        [HttpGet]
        public ActiveDirectoryUser GetUser(string samAccount)
        {

            var adConsumer = new ActiveDirectoryConsumer("opus.local");
            var user = adConsumer.GetUser(samAccount);

            return user;
        }
    }

}