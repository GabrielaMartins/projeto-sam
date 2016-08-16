﻿using DefaultException.Models;
using Newtonsoft.Json;
using SamApiModels.Git;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace SamApi.Helpers
{
    public class GitHelper
    {
        public string User { get; set; }

        public string Password { get; set; }

        public string RepOwner { get; set; }

        public string RepName { get; set; }

        private string CreateOrListUrl;

        public GitHelper(string user, string password, string repName, string repOwner)
        {
            User = user;
            Password = password;
            RepOwner = repOwner;
            RepName = repName;

            CreateOrListUrl = $"https://api.github.com/repos/{repOwner}/{repName}/issues";
        }

        public List<GitIssue> GetIssues(IDictionary<string, string> criterias = null)
        {
            var url = CreateOrListUrl;
            if (criterias != null)
            {
                int i = 1;
                url = string.Concat(url, "?");
                foreach (var element in criterias)
                {
                    var key = element.Key;
                    var value = element.Value;

                    if (key != "filter" &&
                       key != "state" &&
                       key != "labels" &&
                       key != "sort" &&
                       key != "direction" &&
                       key != "since"
                    )
                    {
                        var innerException = new Exception("Accepted criteria: 'filter', 'state', 'labels', 'sort', 'direction', 'since'");
                        throw new Exception("Invalid criteria name", innerException);
                    }

                    switch (key)
                    {
                        case "filter":
                            if (value != "assigned" && value != "created" && value != "mentioned" && value != "subscribed" && value != "all")
                            {
                                throw new Exception("Invalid value for key 'filter'",
                                       new Exception("Accepted values: 'assigned', 'created', 'mentioned', 'subscribed', 'all'"));
                            }
                            break;
                        case "state":
                            if (value != "open" && value != "closed" && value != "all")
                            {
                                throw new Exception("Invalid value for key 'state'",
                                       new Exception("Accepted values: 'open', 'closed', 'all'"));
                            }
                            break;
                        case "labels":
                            var isValid = Regex.IsMatch(value, @"^[-\w\s]+(?:,[-\w\s]*)*$");
                            if (!isValid)
                            {
                                throw new Exception("Invalid value for key 'labels'",
                                       new Exception("Accepted values: a comma separated list of strings"));
                            }
                            break;
                        case "sort":
                            if (value != "created" && value != "updated" && value != "comments")
                            {
                                throw new Exception("Invalid value for key 'sort'",
                                       new Exception("Accepted values: 'created', 'updated', 'comments'"));
                            }
                            break;
                        case "direction":
                            if (value != "asc " && value != "desc")
                            {
                                throw new Exception("Invalid value for key 'direction'",
                                       new Exception("Accepted values: 'asc', 'desc'"));
                            }
                            break;
                        case "since":
                            var ISO_8601 = @"^\d{ 4}(-\d\d(-\d\d(T\d\d:\d\d(:\d\d)?(\.\d +)?(([+-]\d\d:\d\d)|Z)?)?)?)?$";
                            isValid = Regex.IsMatch(value, ISO_8601);
                            if (!isValid)
                            {
                                throw new Exception("Invalid value for key 'since'",
                                       new Exception("Accepted values: a timestamp in ISO 8601 format: YYYY-MM-DDTHH:MM:SSZ"));
                            }
                            break;
                    }


                    url = string.Concat(url, $"{key}={value}");
                    if (i++ < criterias.Count)
                    {
                        url = string.Concat(url, "&");
                    }
                }

            }

            var request = GetRequest(url);
            var response = request.GetResponse() as HttpWebResponse;

            StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(response.CharacterSet));

            return JsonConvert.DeserializeObject<List<GitIssue>>(streamReader.ReadToEnd());
        }

        public bool CreateIssue(string assignee, string title, string body, List<string> labels = null)
        {

            var newIssue = new GitNewIssue()
            {
                assignee = assignee,
                title = title,
                body = body
            };

            var jsonIssue = JsonConvert.SerializeObject(newIssue);

            var request = GetRequest(CreateOrListUrl);
          
            var dataStream = new StreamWriter(request.GetRequestStream());
            dataStream.Write(jsonIssue);
            dataStream.Close();

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            return response.StatusCode == HttpStatusCode.Created;
        }

        private WebRequest GetRequest(string url)
        {
            var authorization = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{User}:{Password}"));
            var request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "Get";
            request.ContentType = "application/json; charset=utf-8";
            request.Accept = "application/json";
            request.Headers.Add("Authorization", "Basic " + authorization);
            request.UserAgent = ("Chrome/51.0.2704.103");

            return request;
        }
    }
}