using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Linq;
using DefaultException.Models;
using SamApi.Attributes.Authorization;
using SamApiModels.Item;
using SamServices.Services;

namespace SamApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api/sam/item")]
    public class SamItemController : ApiController
    {
        // GET: api/sam/item/all
        [Route("all")]
        public HttpResponseMessage Get()
        {

            var itens = ItemServices.RecuperaItens();
            return Request.CreateResponse(HttpStatusCode.OK, itens);

        }

        // GET: api/sam/item/{id}
        [Route("{id}")]
        public HttpResponseMessage Get(int id)
        {
            var itemViewModel = ItemServices.Recupera(id);
            return Request.CreateResponse(HttpStatusCode.OK, itemViewModel);
        }

        // POST: api/sam/item/save
        [Route("save")]
        [SamResourceAuthorizer(Roles = "rh")]
        public HttpResponseMessage Post(ItemViewModel item)
        {
            ItemServices.CriaItem(item);

            return Request.CreateResponse(HttpStatusCode.OK, new DescriptionMessage(HttpStatusCode.OK, "Item Added", "Item Added"));
            
        }

        // PUT: api/sam/item/update/{id}
        [Route("update/{id}")]
        [SamResourceAuthorizer(Roles = "rh")]
        public HttpResponseMessage Put(int id, ItemViewModel item)
        {

            ItemServices.AtualizaItem(id, item);

            return Request.CreateResponse(HttpStatusCode.OK, new DescriptionMessage(HttpStatusCode.OK, "Item Updated", $"Item #{id} Updated"));
            

        }

        // DELETE: api/sam/item/delete/{id}
        [Route("delete/{id}")]
        [SamResourceAuthorizer(Roles = "rh")]
        public HttpResponseMessage Delete(int id)
        {
            ItemServices.DeleteItem(id);
            return Request.CreateResponse(HttpStatusCode.OK, new DescriptionMessage(HttpStatusCode.OK, "Item Deleted", $"Item #{id} Deleted"));
            
        }
    }
}
