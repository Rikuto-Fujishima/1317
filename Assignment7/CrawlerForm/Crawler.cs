﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrawlerForm {
    class Crawler {
        public event Action<Crawler> CrawlerStopped;
        public event Action<Crawler, string, string> PageDownloaded;
        Queue<string> pending = new Queue<string>();
        public Dictionary<string, bool> Downloaded { get; } = new Dictionary<string, bool>();
        public static readonly string UrlDetectRegex = @"(href|HREF)\s*=\s*[""'](?<url>[^""'#>]+)[""']";
        public static readonly string urlParseRegex = @"^(?<site>(?<protocal>https?)://(?<host>[\w\d.-]+)(:\d+)?($|/))(\w+/)*(?<file>[^#?]*)";
        public string HostFilter { get; set; }
        public string FileFilter { get; set; }
        public int MaxPage { get; set; }
        public string StartURL { get; set; }
        public Encoding HtmlEncoding { get; set; }
        public Crawler() {
            MaxPage = 100;
            HtmlEncoding = Encoding.UTF8;
        }
        public void Start() {
            Downloaded.Clear();
            pending.Clear();
            pending.Enqueue(StartURL);
            while (Downloaded.Count < MaxPage && pending.Count > 0) {
                string url = pending.Dequeue();
                try {
                    string html = DownLoad(url); 
                    Downloaded[url] = true;
                    PageDownloaded(this, url, "success");
                    Parse(html, url);
                } catch (Exception ex) {
                    Downloaded[url] = false;
                    PageDownloaded(this, url, "  Error:" + ex.Message);
                }
            }
            CrawlerStopped(this);
        }
        private string DownLoad(string url) {
            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            string html = webClient.DownloadString(url);
            string fileName = Downloaded.Count.ToString();
            File.WriteAllText(fileName, html, Encoding.UTF8);
            return html;
        }
        private void Parse(string html, string pageUrl) {
            var matches = new Regex(UrlDetectRegex).Matches(html);
            foreach (Match match in matches) {
                string linkUrl = match.Groups["url"].Value;
                if (linkUrl == null || linkUrl == "" || linkUrl.StartsWith("javascript:")) continue;
                linkUrl = FixUrl(linkUrl, pageUrl);
                Match linkUrlMatch = Regex.Match(linkUrl, urlParseRegex);
                string host = linkUrlMatch.Groups["host"].Value;
                string file = linkUrlMatch.Groups["file"].Value;
                if (Regex.IsMatch(host, HostFilter) && Regex.IsMatch(file, FileFilter)
                  && !Downloaded.ContainsKey(linkUrl) && !pending.Contains(linkUrl)) {
                    pending.Enqueue(linkUrl);
                }
            }
        }
        static private string FixUrl(string url, string pageUrl) {
            if (url.Contains("://")) { 
                return url;
            }
            if (url.StartsWith("//")) {
                Match urlMatch = Regex.Match(pageUrl, urlParseRegex);
                string protocal = urlMatch.Groups["protocal"].Value;
                return protocal + ":" + url;
            }
            if (url.StartsWith("/")) {
                Match urlMatch = Regex.Match(pageUrl, urlParseRegex);
                String site = urlMatch.Groups["site"].Value;
                return site.EndsWith("/") ? site + url.Substring(1) : site + url;
            }
            if (url.StartsWith("../")) {
                url = url.Substring(3);
                int idx = pageUrl.LastIndexOf('/');
                return FixUrl(url, pageUrl.Substring(0, idx));
            }

            if (url.StartsWith("./")) {
                return FixUrl(url.Substring(2), pageUrl);
            }
            int end = pageUrl.LastIndexOf("/");
            return pageUrl.Substring(0, end) + "/" + url;
        }
    }
}
