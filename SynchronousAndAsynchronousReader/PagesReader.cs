using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SynchronousAndAsynchronousReader
{
    public class PagesReader
    {
        private readonly HttpClient _httpClient = new HttpClient();
        public List<string> Pages = new List<string>();
        public string pageConten = "";
        public async Task<string> ReadPage(string url)
        {
            pageConten = "";
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                var dataObjects = response.Content.ReadAsStringAsync().Result;
                pageConten = dataObjects.ToString();
                //findAndListAllReferencesLinked(pageConten);
            }
            //Console.WriteLine(pageConten);
            return pageConten;
        }

        public void findAndListAllReferencesLinked(string pageContent)
        {
            string toFind = "href=";
            string output = pageContent;
            string readedHtml="";
            for (int i = 0; i < pageContent.Length; i++)
            {
                if(i< pageContent.Length-10)
                {
                    if(pageContent[i] == 'h' && pageContent[i+1] == 'r' && pageContent[i + 2] == 'e' && pageContent[i + 3] == 'f' && pageContent[i + 4] == '=')
                    {
                        i=i+6;
                        if(pageContent[i]=='h' && pageContent[i+1] == 't')
                        {
                            while (pageContent[i] != 34)
                            {
                                readedHtml += pageContent[i];
                                i++;
                            }
                            Pages.Add(readedHtml);
                            readedHtml = "";
                        }
                        
                    }
                }
            }
             
        }
    }
}
