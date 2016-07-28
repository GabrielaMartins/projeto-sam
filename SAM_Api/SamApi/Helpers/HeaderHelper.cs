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
                throw new ExpectedException(HttpStatusCode.BadRequest, ex.Message, "Key '" + key + "' not found.");
            }
        }

    }
}