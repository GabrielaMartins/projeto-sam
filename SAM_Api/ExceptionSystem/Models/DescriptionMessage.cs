using System.Collections.Generic;
using System.Net;

namespace DefaultException.Models
{
    public class DescriptionMessage
    {

        public int Code { get; set; }

        public string Title { get; set; }

        public List<string> Details { get; set; }


        public DescriptionMessage(HttpStatusCode code, string title, string detail="")
        {
            Code = (int)code;
            Title = title;
            Details = new List<string>(){detail};
        }

        public DescriptionMessage(int code, string title, string detail="")
        {
            Code = code;
            Title = title;
            Details = new List<string>() { detail };
        }

        public DescriptionMessage(HttpStatusCode code, string title, List<string> details)
        {
            Code = (int)code;
            Title = title;
            Details = details;
        }

        public DescriptionMessage(int code, string title, List<string> details)
        {
            Code = code;
            Title = title;
            Details = details;
        }
    }
}