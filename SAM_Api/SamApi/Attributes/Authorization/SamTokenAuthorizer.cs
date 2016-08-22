using DefaultException.Models;
using Opus.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace SamApi.Attributes.Authorization
{
    public class SamTokenAuthorizer : AuthorizeAttribute
    {

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            // if un debug mode, we skip token validator
            //#if DEBUG
            //    return true;
            //#endif

            return ValidateToken(ref actionContext);
        }

        private bool ValidateToken(ref HttpActionContext actionContext)
        {

            // if the request is to login, just return true
            if(actionContext.Request.RequestUri.AbsoluteUri.Contains("api/sam/login")){
                return true;
            }

            // this lines throw exception (if an error) captured by our filter
            var token = ExtractHeaderValue(actionContext.Request, "token");
            var decodedToken = JwtHelper.DecodeToken(token.SingleOrDefault());

            // here we need to create an exception if we could not find some keys on decoded token
            try
            {
                var context = decodedToken["context"] as Dictionary<string, object>;
                var user = context["user"] as Dictionary<string, object>;
                var id = user["id"];
                var perfil = user["perfil"] as string;
                var samaccount = user["samaccount"] as string;

                // we are modifying the original request
                actionContext.Request.Headers.Add("id", Convert.ToString(id));
                actionContext.Request.Headers.Add("perfil", perfil);
                actionContext.Request.Headers.Add("samaccount", samaccount);
                actionContext.Request.Headers.Remove("token");

                return true;
            }
            catch
            {
                throw new ExpectedException(HttpStatusCode.Unauthorized, "Token Invalid", "The provided token has a valid signature, but has an invalid content");
            }
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