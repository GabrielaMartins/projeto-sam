using ExceptionSystem.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;

namespace Opus.Helpers
{
    public class HeaderHelper
    {

        public static IEnumerable<string> ExtractHeaderValue(HttpRequestMessage request, string key)
        {

            try
            {
                return request.Headers.GetValues(key);
            }
            catch(Exception ex)
            {
                var e = new Exception(ex.Message, new Exception(" key '" + key + "' is not present"));
                throw new ExpectedException((int)HttpStatusCode.BadRequest, "Invalid Header", e);
            }
        }

    }
}