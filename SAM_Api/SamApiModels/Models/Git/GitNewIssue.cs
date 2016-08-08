using System;
using System.Collections.Generic;

namespace SamApiModels.Git
{
    [Serializable]
    public class GitNewIssue
    {
        public string title { get; set; }

        public string body { get; set; }

        public string assignee { get; set; }

        //public int milestone { get; set; }

        public List<string> labels { get; set; }

        //public List<string> assignees { get; set; }

        public GitNewIssue()
        {
            labels = new List<string>();
            //assignees = new List<string>();
        }

    }
}
