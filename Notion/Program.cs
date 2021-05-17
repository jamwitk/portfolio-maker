using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using System.Net;
using RestSharp;

namespace Notion
{
    static class Program
    {
        public static void Main(string[] args)
        {
            
            DataFromPlayStore();
            
            Console.ReadKey();
            
        }

        private static string databaseId = "{DATABASE_ID}";
        private static string token = "{AUTHORIZATION_TOKEN_ID}";
        private static string _url = "";
        private static void DataFromPlayStore()
        {
            Console.Write("Play Store Games URL: ");
            _url = Console.ReadLine();
            var webClient = new WebClient();
            string downloadedString = webClient.DownloadString(_url ?? string.Empty);
            ParseHtml(downloadedString);
        }
        private static void ParseHtml(string downloadedFile)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(downloadedFile);
            var htmlContent = htmlDocument.DocumentNode;
            
            var iconUrl = GetIconUrl(htmlContent);
            var title = GetTitle(htmlContent);
            var description = GetDescription(htmlContent);
            
            SendToNotion(iconUrl,title,description,_url);
        }

        private static void SendToNotion(string icon, string title, string description,string url)
        {
           var client = new RestClient("https://api.notion.com/v1/pages/");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"{token}");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Notion-Version", "2021-05-13");
            var body = 
                        @"{" + "\n" +
                       @"	""parent"": { ""database_id"": """+databaseId+'"'+ "}," + "\n" +
                       @"	""properties"": {" + "\n" +
                       @"		""Name"": {" + "\n" +
                       @"			""title"": [" + "\n" +
                       @"				{" + "\n" +
                       @"					""text"": {" + "\n" +
                       @"						""content"": """+title+'"'+"\n"+
                       @"					}" + "\n" +
                       @"				}" + "\n" +
                       @"			]" + "\n" +
                       @"		}," + "\n" +
                       @"        ""Link"": {" + "\n" +
                       @"            ""url"": """+url+'"'+ "\n" +
                       @"        }" + "\n" +
                       @"	}," + "\n" +
                       @"	""children"": [" + "\n" +
                       @"		{" + "\n" +
                       @"			""object"": ""block""," + "\n" +
                       @"			""type"": ""heading_2""," + "\n" +
                       @"			""heading_2"": {" + "\n" +
                       @"				""text"": [{ ""type"": ""text"", ""text"": { ""content"": ""Description"" } }]" + "\n" +
                       @"			}" + "\n" +
                       @"		}," + "\n" +
                       @"		{" + "\n" +
                       @"			""object"": ""block""," + "\n" +
                       @"			""type"": ""paragraph""," + "\n" +
                       @"			""paragraph"": {" + "\n" +
                       @"				""text"": [" + "\n" +
                       @"					{" + "\n" +
                       @"						""type"": ""text""," + "\n" +
                       @"						""text"": {" + "\n" +
                       @"							""content"": """+description+'"'+"\n" +
                       @"						}" + "\n" +
                       @"					}" + "\n" +
                       @"				]" + "\n" +
                       @"			}" + "\n" +
                       @"		}" + "\n" +
                       @"	]" + "\n" +
                       @"}";
            request.AddParameter("application/json", body,  ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            
        }
        private static string GetIconUrl(HtmlNode content)
        {
            foreach (var node in content.SelectNodes("//img[@class='T75of sHb2Xb']"))
            {
             return node.SelectSingleNode("//img").Attributes["src"].Value;
            }
            return null;
        }

        private static string GetTitle(HtmlNode content)
        {
            return content.SelectNodes("//h1[@class='AHFaub']").Select(node => node.InnerText).FirstOrDefault();
        }

        private static List<string> GetImages(HtmlNode content)
        {
            var ls = new List<string>();
            foreach (var node in content.SelectNodes("//img[@class='T75of DYfLw']"))
            {
                ls.Add(node.GetAttributeValue("data-src","") == "" ? node.GetAttributeValue("src","") : node.GetAttributeValue("data-src",""));
            }

            return ls;
        }

        private static string GetDescription(HtmlNode content)
        {
            foreach (var node in content.SelectNodes("//div[@jsname='sngebd']"))
            {
                return node.InnerText;
            }
            return null;
        }
    }
    
}