﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using DocsScraper;

namespace Tests
{
    public class MockSiteRequester : ISiteRequester
    {
        private Dictionary<string, string> urls = new Dictionary<string, string>
        {
            {"https://deskportal.zoho.com/portal/engineerica/kb/tags/accudemia", "Tests.HtmlResults.article-list.html"},
            {"https://deskportal.zoho.com/portal/engineerica/kb/articles/what-do-i-do-if-the-accudemia-website-will-not-load", "Tests.HtmlResults.article.html"}
        };

        public TimeSpan? RequestDelay { get; set; }

        public string GetHtml(string url)
        {
            if (RequestDelay != null)
            {
                Thread.Sleep(RequestDelay.Value);
            }


            if (!urls.ContainsKey(url)) throw new FileNotFoundException("Cannot request URL: " + url);

            var file = urls[url];
            return LoadResource(file);
        }

        private static string LoadResource(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
