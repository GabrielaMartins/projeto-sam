﻿using MessageSystem.Erro;
using SamHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace SamApi.Attributes.Authorization
{
    /// <summary>
    /// Autoriza ou nega requisições baseado no token
    /// </summary>
    public class SamTokenAuthorizer : AuthorizeAttribute
    {
        /// <summary>
        /// Diz se a requisição pode ou não ser autorizada
        /// </summary>
        /// <param name="actionContext">è o contexto da requisição</param>
        /// <returns></returns>
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
            var token = ExtractToken(actionContext.Request);
            var decodedToken = JwtHelper.DecodeToken(token);

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
                throw new ErroEsperado(HttpStatusCode.Unauthorized, "Token Invalid", "The provided token has a valid signature, but has an invalid content");
            }
        }

        private static string ExtractToken(HttpRequestMessage request)
        {

            try
            {
                var token =  request.Headers.GetValues("token").SingleOrDefault();
                if (token == null || token == "null")
                {
                    throw new ErroEsperado(HttpStatusCode.BadRequest, "Token empty", "You must provide a valid token");
                }
                else
                {
                    return token;
                }
            }
            catch (Exception ex)
            {
                throw new ErroEsperado(HttpStatusCode.BadRequest, ex.Message, "Key 'token' not found.");
            }
        }
    }
}