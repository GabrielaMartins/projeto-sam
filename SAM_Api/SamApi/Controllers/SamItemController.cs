using System.Web.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System;
using SamApi.Helpers;
using SamApiModels;

namespace SamApi.Controllers
{
    [RoutePrefix("api/sam/item")]
    public class SamItemController : ApiController
    {
        // GET: api/sam/item/all
        [Route("all")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/sam/item/{id}
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
            response = Request.CreateResponse(HttpStatusCode.OK, new MessageViewModel(HttpStatusCode.ServiceUnavailable, "Not Implemented", "under construction"));
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };

            return response;
        }

        // POST: api/sam/item/save
        [Route("save")]
        public HttpResponseMessage Post([FromBody]string item)
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
            response = Request.CreateResponse(HttpStatusCode.OK, new MessageViewModel(HttpStatusCode.ServiceUnavailable, "Not Implemented", "under construction"));
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };

            return response;
        }

        // PUT: api/sam/item/update/{id}
        [Route("update/{id}")]
        public HttpResponseMessage Put(int id, [FromBody]string item)
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
            response = Request.CreateResponse(HttpStatusCode.OK, new MessageViewModel(HttpStatusCode.ServiceUnavailable, "Not Implemented", "under construction"));
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };

            return response;

        }

        // DELETE: api/sam/item/delete/{id}
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
            response = Request.CreateResponse(HttpStatusCode.OK, new MessageViewModel(HttpStatusCode.ServiceUnavailable, "Not Implemented", "under construction"));
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };

            return response;
        }
    }
}
