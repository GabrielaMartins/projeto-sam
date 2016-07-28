using System;
using System.Collections.Generic;
using SamApiModels;
using DefaultException.Models;
using System.Net;
using System.Configuration;

namespace Opus.Helpers
{

    public class JwtHelper
    {

        private static readonly string plainTextSecurityKey = ConfigurationManager.AppSettings["SecurityKey"];

        public JwtHelper()
        {

        }

        public static string GenerateToken(UsuarioViewModel user)
        {

            if (user == null)
                return string.Empty;


            // variables to configure our token
            var currentTime = DateTime.Now;
            //var expTime = 50000;
            Dictionary<string, object> context = null;
            Dictionary<string, object> userInfo = null;
            Dictionary<string, object> payload = null;

            userInfo = new Dictionary<string, object>()
            {
                {"id", user.id},
                {"perfil", user.perfil},
                {"samaccount", user.samaccount}

                // we can put more information here
            };

            context = new Dictionary<string, object>()
            {
                {"user", userInfo},
                // we can put more information here
            };

            payload = new Dictionary<string, object>()
            {
                // informa o cliente quem emitiu o token
                { "iss", "http://opus.sam.com" },

                // informa a data e hora que o token foi emitido
                { "iat", currentTime},

                // esse é o tempo de vida do token (da erro quando decodifica, nao sei pq)
                //{ "exp", currentTime.AddHours(expTime)},

                // contém informações que queremos colocar no token, como usuário por exemplo
                { "context", context }

            };

            string plainToken = JWT.JsonWebToken.Encode(payload, plainTextSecurityKey, JWT.JwtHashAlgorithm.HS256);

            return plainToken;
        }

        public static IDictionary<string, object> DecodeToken(string token)
        {

            try
            {
                var res = JWT.JsonWebToken.DecodeToObject(token, plainTextSecurityKey) as IDictionary<string, object>;
                return res;
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("signature"))
                {
                    throw new ExpectedException(HttpStatusCode.Unauthorized, "Invalid Token");
                }

                throw ex;
                
            }
            
        }

    }
}