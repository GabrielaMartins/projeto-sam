using Opus.Helpers.Http;
using Opus.Helpers.Security;
using SamApiModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;


namespace SamApi.Helpers
{
    public class CommonOperations
    {

        private HttpRequestMessage request;

        public IDictionary<string, object> DecodedToken;

        public HttpResponseMessage ResponseError;


        public CommonOperations(HttpRequestMessage request)
        {
            this.request = request;
            this.ResponseError = null;
        }

        public void Check()
        {

            // always request the token (melhorar o metodo abaixo)
            var token = HeaderHelper.GetHeaderValues(request, "token") as string;
            if (token == null)
            {
                // error
                ResponseError = request.CreateResponse(HttpStatusCode.BadRequest, MessageViewModel.TokenMissing);
                ResponseError.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    MaxAge = TimeSpan.FromMinutes(20)
                };

                return;
            }

            // decode our token
            try
            {
                var tk = JwtManagement.DecodeToken(token);
                DecodedToken = tk;

            }
            catch
            {
                ResponseError = request.CreateResponse(HttpStatusCode.Unauthorized, MessageViewModel.InvalidToken);
                ResponseError.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    MaxAge = TimeSpan.FromMinutes(20)
                };
            }
        }
    }
}