using System.Web.Http;
using Opus.Helpers.Http;
using System.Collections.Generic;
using System.Linq;
using SamApi.Models;
using Opus.Helpers.Security;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System;

namespace SamApi.Controllers
{
    [RoutePrefix("api/sam/user")]
    public class SamUserController : ApiController
    {
        // GET: api/sam/user/all
        [Route("all")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/sam/user/{id}
        [Route("{id}")]
        public HttpResponseMessage Get(int id)
        {

            HttpResponseMessage response = null;

            // always request the token (melhorar o metodo abaixo)
            var token = HeaderHelper.GetHeaderValues(Request, "token") as string;
            if(token == null)
            {
                // error
                var res = new Message(HttpStatusCode.BadRequest, "invalid header", "The server cannot find the token in http header");
                response = Request.CreateResponse(HttpStatusCode.BadRequest, res);
                response.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    MaxAge = TimeSpan.FromMinutes(20)
                };

                return response;
            }

            var payload = JwtManagement.DecodeToken(token);
            if(payload == null)
            {
                // error
                var res = new Message(HttpStatusCode.Unauthorized, "invalid token", "you have no permission here because your token is invalid");
                response = Request.CreateResponse(HttpStatusCode.Unauthorized, res);
                response.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    MaxAge = TimeSpan.FromMinutes(20)
                };

                return response;
            }

            // returns the user
            response = Request.CreateResponse(HttpStatusCode.OK, new User("teste", "teste de get", "email"));
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };

            return response;
        }

        // POST: api/sam/user/save
        [Route("save")]
        public void Post([FromBody]User user)
        {
        }

        // PUT: api/sam/user/update/{id}
        [Route("update/{id}")]
        public void Put(int id, [FromBody]User user)
        {

        }

        // DELETE: api/sam/user/delete/{id}
        [Route("delete/{id}")]
        public void Delete(int id)
        {
        }
    }
}
