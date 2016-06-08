using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Opus.Helpers.Http
{
    public class HeaderHelper
    {

        public static object GetHeaderValues(HttpRequestMessage request, string headerKey)
        {

            IEnumerable<string> headerValues;
            if (request.Headers.TryGetValues(headerKey, out headerValues))
            {
                return request.Headers.GetValues(headerKey).FirstOrDefault();
            }

            return null;
        }

    }
}