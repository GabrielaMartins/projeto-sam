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
using SamApi.Helpers;
using SamDataBase.Model;

namespace SamApi.Controllers
{
    [RoutePrefix("api/sam/user")]
    public class SamUserController : ApiController
    {
        // GET: api/sam/user/all
        [Route("all")]
        public HttpResponseMessage Get()
        {
            CommonOperations commonOperations = new CommonOperations(Request);
            HttpResponseMessage response = null;

            // this line check and prepare some variables for us
            commonOperations.Check();

            // if we have response, so it's an error
            if (commonOperations.ResponseError != null)
                return commonOperations.ResponseError;

            var token = commonOperations.DecodedToken;

            // fill here

            // ********* //

            // erase here
            response = Request.CreateResponse(HttpStatusCode.OK, new Message(HttpStatusCode.ServiceUnavailable, "Not Implemented", "under construction"));
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };

            return response;
        }

        // GET: api/sam/user/{id}
        [Route("{id}")]
        public HttpResponseMessage Get(int id)
        {

            CommonOperations commonOperations = new CommonOperations(Request);
            HttpResponseMessage response = null;

            // this line check and prepare some variables for us
            commonOperations.Check();

            // if we have response, so it's an error
            if (commonOperations.ResponseError != null)
                return commonOperations.ResponseError;

            var token = commonOperations.DecodedToken;

            // erase here
            response = Request.CreateResponse(HttpStatusCode.OK, new Message(HttpStatusCode.ServiceUnavailable, "Not Implemented", "under construction"));
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };

            return response;
        }

        // POST: api/sam/user/save
        [Route("save")]
        public HttpResponseMessage Post([FromBody]Usuario user)
        {

            CommonOperations commonOperations = new CommonOperations(Request);
            HttpResponseMessage response = null;

            // this line check and prepare some variables for us
            commonOperations.Check();

            // if we have response, so it's an error
            if (commonOperations.ResponseError != null)
                return commonOperations.ResponseError;

            var token = commonOperations.DecodedToken;

            // erase here
            response = Request.CreateResponse(HttpStatusCode.OK, new Message(HttpStatusCode.ServiceUnavailable, "Not Implemented", "under construction"));
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };

            return response;
        }

        // PUT: api/sam/user/update/{id}
        [Route("update/{id}")]
        public HttpResponseMessage Put(int id, [FromBody]Usuario user)
        {

            CommonOperations commonOperations = new CommonOperations(Request);
            HttpResponseMessage response = null;

            // this line check and prepare some variables for us
            commonOperations.Check();

            // if we have response, so it's an error
            if (commonOperations.ResponseError != null)
                return commonOperations.ResponseError;

            var token = commonOperations.DecodedToken;

            // erase here
            response = Request.CreateResponse(HttpStatusCode.OK, new Message(HttpStatusCode.ServiceUnavailable, "Not Implemented", "under construction"));
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };

            return response;

        }

        // DELETE: api/sam/user/delete/{id}
        [Route("delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {

            CommonOperations commonOperations = new CommonOperations(Request);
            HttpResponseMessage response = null;

            // this line check and prepare some variables for us
            commonOperations.Check();

            // if we have response, so it's an error
            if (commonOperations.ResponseError != null)
                return commonOperations.ResponseError;

            var token = commonOperations.DecodedToken;

            // erase here
            response = Request.CreateResponse(HttpStatusCode.OK, new Message(HttpStatusCode.ServiceUnavailable, "Not Implemented", "under construction"));
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };

            return response;
        }
    }
}
