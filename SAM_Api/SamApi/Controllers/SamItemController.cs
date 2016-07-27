using System.Web.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System;
using SamApiModels;
using Opus.DataBaseEnvironment;

namespace SamApi.Controllers
{
    [RoutePrefix("api/sam/item")]
    public class SamItemController : ApiController
    {
        // GET: api/sam/item/all
        [Route("all")]
        public HttpResponseMessage Get()
        {
            using(var itemRep = DataAccess.Instance.GetItemRepository())
            {
                var itens = itemRep.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, itens);
            }
        }

        // GET: api/sam/item/{id}
        [Route("{id}")]
        public HttpResponseMessage Get(int id)
        {

            using (var itemRep = DataAccess.Instance.GetItemRepository())
            {
                var item = itemRep.Find(i => i.id == id);
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
        }

        // POST: api/sam/item/save
        [Route("save")]
        public HttpResponseMessage Post([FromBody]string item)
        {

            // erase here
            var response = Request.CreateResponse(HttpStatusCode.OK, new MessageViewModel(HttpStatusCode.ServiceUnavailable, "Not Implemented", "under construction"));
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
            
            // erase here
            var response = Request.CreateResponse(HttpStatusCode.OK, new MessageViewModel(HttpStatusCode.ServiceUnavailable, "Not Implemented", "under construction"));
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
          
            // erase here
            var response = Request.CreateResponse(HttpStatusCode.OK, new MessageViewModel(HttpStatusCode.ServiceUnavailable, "Not Implemented", "under construction"));
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };

            return response;
        }
    }
}
