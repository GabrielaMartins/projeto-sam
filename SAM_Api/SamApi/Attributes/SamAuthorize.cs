using DefaultException.Models;
using Opus.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace SamApi.Attributes
{
    public class SamAuthorize : AuthorizeAttribute
    {
       
        public SamAuthorize()
        {
         
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {

            var token = ExtractHeaderValue(actionContext.Request, "token");
            var decodedToken = JwtHelper.DecodeToken(token.SingleOrDefault());
            var context = decodedToken["context"] as Dictionary<string, object>;
            var user = context["user"] as Dictionary<string, object>;
            var perfil = user["perfil"] as string;
            var samaccount = (context["user"] as Dictionary<string, object>)["samaccount"] as string;

            if (Roles.ToLowerInvariant().Contains(perfil.ToLowerInvariant()) || 
                Users.ToLowerInvariant().Contains(samaccount.ToLowerInvariant()))
            {
                // add to request
                actionContext.Request.Headers.Add("samaccount", samaccount);
                actionContext.Request.Headers.Add("perfil", perfil);
                return true;
            }

            return false;
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            throw new ExpectedException(HttpStatusCode.Unauthorized, "Unauthorized", "User has no permission");
           
        }

        private static IEnumerable<string> ExtractHeaderValue(HttpRequestMessage request, string key)
        {

            try
            {
                return request.Headers.GetValues(key);
            }
            catch (Exception ex)
            {
                throw new ExpectedException(HttpStatusCode.BadRequest, ex.Message, "Key '" + key + "' not found.");
            }
        }
    }
}