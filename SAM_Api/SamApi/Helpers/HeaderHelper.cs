using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

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
            catch
            {
                return null;
            }
        }

    }
}