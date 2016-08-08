using System.Web.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System;
using SamApiModels;
using Opus.DataBaseEnvironment;
using Opus.Helpers;
using System.Linq;
using AutoMapper;
using SamDataBase.Model;
using DefaultException.Models;
using SamApi.Attributes;
using SamApiModels.Item;
using SamApiModels.Message;

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
        [SamAuthorize(Roles="rh")]
        public HttpResponseMessage Post(ItemViewModel item)
        {

            using (var itemRep = DataAccess.Instance.GetItemRepository())
            {
                // map new values to our reference
                var newItem = Mapper.Map<ItemViewModel, Item>(item);

                // add to entity context
                itemRep.Add(newItem);

                // commit changes
                itemRep.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK, new MessageViewModel(HttpStatusCode.OK, "Item Added", "Item Added"));
            }
        }

        // PUT: api/sam/item/update/{id}
        [Route("update/{id}")]
        [SamAuthorize(Roles = "rh")]
        public HttpResponseMessage Put(int id, ItemViewModel item)
        {

            using (var itemRep = DataAccess.Instance.GetItemRepository())
            {
                var itemToBeUpdated = itemRep.Find(i => i.id == id).SingleOrDefault();
                if(itemToBeUpdated == null)
                {
                    throw new ExpectedException(HttpStatusCode.NotFound, "Item Not Found", $"Item #{id} not found");
                }

                // map new values to our reference
                itemToBeUpdated = Mapper.Map(item, itemToBeUpdated);

                // add to entity context
                itemRep.Update(itemToBeUpdated);

                // commit changes
                itemRep.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK, new MessageViewModel(HttpStatusCode.OK, "Item Updated", $"Item #{id} Updated"));
            }

        }

        // DELETE: api/sam/item/delete/{id}
        [Route("delete/{id}")]
        [SamAuthorize(Roles = "rh")]
        public HttpResponseMessage Delete(int id)
        {

            using (var itemRep = DataAccess.Instance.GetItemRepository())
            {
                itemRep.Delete(id);

                return Request.CreateResponse(HttpStatusCode.OK, new MessageViewModel(HttpStatusCode.OK, "Item Deleted", $"Item #{id} Deleted"));
            }
        }
    }
}
