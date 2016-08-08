﻿using DefaultException.Models;
using Opus.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace SamApi.Attributes
{
    public class SamAuthorize : AuthorizeAttribute
    {
       
        public enum AuthType
        {
            Basic,
            UpdateUser
        }

        public AuthType AuthorizationType { get; set; }

        private IDictionary<string, object> DecodedToken;

        private string ErrorMessage;

        public SamAuthorize(AuthType authorizationType = AuthType.Basic )
        {
            ErrorMessage = "User has no permission";
            AuthorizationType = authorizationType;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            // configure some header and decode our token
            ConfigureRequest(ref actionContext);

            bool isComplexAuthorized = true;
            bool isBasicAuthorized = BasicAuthorization();
            if(AuthorizationType == AuthType.Basic)
            {
                return isBasicAuthorized;
            }
                       

            switch (AuthorizationType)
            {
                case AuthType.UpdateUser:
                    isComplexAuthorized = AuthorizeUpdateUser(actionContext);
                    ErrorMessage = "You can't update other users";
                    break;
            }

            return (isBasicAuthorized && isComplexAuthorized);
            
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            throw new ExpectedException(HttpStatusCode.Unauthorized, "Unauthorized", ErrorMessage);

        }

        private bool AuthorizeUpdateUser(HttpActionContext context)
        {
            var tokenContext = DecodedToken["context"] as Dictionary<string, object>;

            // data from user making the request
            var user = tokenContext["user"] as Dictionary<string, object>;
            var id = (int)user["id"];
            var perfil = user["perfil"] as string;

            // user to be updated
            var size = context.Request.RequestUri.Segments.Length;
            if(size >= 1)
            {
                var userId = Convert.ToInt32(context.Request.RequestUri.Segments[size - 1]);
                return (userId == id || perfil == "rh");
            }

            return false;
        }

        private bool BasicAuthorization()
        {

            var context = DecodedToken["context"] as Dictionary<string, object>;
            var user = context["user"] as Dictionary<string, object>;
            var perfil = user["perfil"] as string;
            var samaccount = user["samaccount"] as string;

            return (
                Roles.ToLowerInvariant().Contains(perfil.ToLowerInvariant()) ||
                Users.ToLowerInvariant().Contains(samaccount.ToLowerInvariant())
            );
            
        }

        private void ConfigureRequest(ref HttpActionContext actionContext)
        {
            var token = ExtractHeaderValue(actionContext.Request, "token");
            var decodedToken = JwtHelper.DecodeToken(token.SingleOrDefault());
            var context = decodedToken["context"] as Dictionary<string, object>;
            var user = context["user"] as Dictionary<string, object>;
            var id = user["id"];
            var perfil = user["perfil"] as string;
            var samaccount = user["samaccount"] as string;

            actionContext.Request.Headers.Add("id", Convert.ToString(id));
            actionContext.Request.Headers.Add("perfil", perfil);
            actionContext.Request.Headers.Add("samaccount", samaccount);
            DecodedToken = decodedToken;
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