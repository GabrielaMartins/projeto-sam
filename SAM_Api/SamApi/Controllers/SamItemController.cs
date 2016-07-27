﻿using System.Web.Http;
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
using System.Data.Entity.Validation;

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
        public HttpResponseMessage Post(ItemViewModel item)
        {

            var token = HeaderHelper.ExtractHeaderValue(Request, "token");
            var decodedToken = JwtHelper.DecodeToken(token.SingleOrDefault());
            var context = decodedToken["context"] as Dictionary<string, object>;
            var perfil = context["perfil"] as string;

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
        public HttpResponseMessage Put(int id, ItemViewModel item)
        {

            var token = HeaderHelper.ExtractHeaderValue(Request, "token");
            var decodedToken = JwtHelper.DecodeToken(token.SingleOrDefault());
            var context = decodedToken["context"] as Dictionary<string, object>;
            var perfil = context["perfil"] as string;

            using (var itemRep = DataAccess.Instance.GetItemRepository())
            {
                var itemToBeUpdated = itemRep.Find(i => i.id == id).SingleOrDefault();
                if(itemToBeUpdated == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, new MessageViewModel(HttpStatusCode.NotFound, "Item Not Found", "Item with id '" + id + "' not found"));
                }

                // map new values to our reference
                itemToBeUpdated = Mapper.Map<ItemViewModel, Item>(item, itemToBeUpdated);

                // add to entity context
                itemRep.Update(itemToBeUpdated);

                // commit changes
                itemRep.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK, new MessageViewModel(HttpStatusCode.OK, "Item Updated", "Item Updated"));
            }

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
