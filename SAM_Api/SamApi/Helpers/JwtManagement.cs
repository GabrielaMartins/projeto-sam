using System;
using System.Collections.Generic;
using SamApi.Models;
using SamApi.Helpers;
using SamDataBase.Model;

namespace Opus.Helpers.Security
{

    public class JwtManagement
    {

        private static readonly string plainTextSecurityKey = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk45Xdlgsyfc";

        public JwtManagement()
        {

        }

        public static string GenerateToken(Usuario user)
        {

            if (user == null)
                return String.Empty;


            // variables to configure our token
            var currentTime = DateTime.Now;
            //var expTime = 50000;
            Dictionary<string, object> context = null;
            Dictionary<string, object> userInfo = null;
            Dictionary<string, object> payload = null;

            userInfo = new Dictionary<string, object>()
            {
                {"id", user.id},
                {"nome", user.nome },
                {"samaccount", user.samaccount}

                // we can put more information here
            };

            context = new Dictionary<string, object>()
            {
                {"user", userInfo}

                // we can put more information here
            };

            payload = new Dictionary<string, object>()
            {
                // informa o cliente quem emitiu o token
                { "iss", "http://opus.sam.com" },

                // informa a data e hora que o token foi emitido
                { "iat", currentTime},

                /// esse é o tempo de vida do token (da erro quando decodifica, nao sei pq)
                //{ "exp", currentTime.AddHours(expTime)},

                // assunto do token
                { "sub", user.nome},

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
            catch (Exception e)
            {
                throw e;
            }
            
        }

    }
}