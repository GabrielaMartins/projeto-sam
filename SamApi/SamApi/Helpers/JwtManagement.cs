using System;
using System.Collections.Generic;
using SamApi.Models;

namespace Opus.Helpers.Security
{

    public class JwtManagement
    {

        private static readonly string plainTextSecurityKey = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";

        public JwtManagement()
        {

        }

        public static string GenerateToken(User user)
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
                {"key", user.Key},
                {"name", user.Name},
                {"email", user.Email}

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

                // informa a dasta e hora que o token foi emitido
                { "iat", currentTime},

                /// esse é o tempo de vida do token (da erro)
                //{ "exp", currentTime.AddHours(expTime)},

                // assunto do token
                { "sub", user.Name},

                // contém informações que queremos colocar no token, como usuário por exemplo
                { "context", context }

            };

            string plainToken = JWT.JsonWebToken.Encode(payload, plainTextSecurityKey, JWT.JwtHashAlgorithm.HS256);

            return plainToken;
        }

        public static IDictionary<string, object> DecodeToken(string token)
        {

            IDictionary<string, object> payload = null;
            try
            {
                payload = JWT.JsonWebToken.DecodeToObject(token, plainTextSecurityKey) as IDictionary<string, object>;
            }
            catch (JWT.SignatureVerificationException)
            {
                Console.WriteLine("Invalid token!");
            }

            return payload;
        }

    }
}