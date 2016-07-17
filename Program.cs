using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebHttpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            GetRequest("https://posttestserver.com/post.php");
            //PostRequest("https://posttestserver.com/post.php");
            Console.ReadLine();
        }

        async static void GetRequest(String url)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    using (HttpContent content = response.Content)
                    {
                        string myContent = await content.ReadAsStringAsync();
                        Console.Write(myContent);

                        HttpContentHeaders headers = content.Headers;

                        Console.Write(headers);
                        if (headers.Contains("Content-Length"))
                        {
                            IEnumerable<string> headerValues = content.Headers.GetValues("Content-Length");
                            var id = headerValues.FirstOrDefault();
                            Console.WriteLine("Response variable" + id);
                        }

                        Console.ReadKey();
                    }

                    //HttpResponseHeaders responseheader = response.Headers;
                    //IEnumerable<string> myResponseHeader = responseheader.GetValues("Server");
                    //Console.Write(myResponseHeader);
                    //if (myResponseHeader.Contains("Apache"))
                    //{
                    //    var id = myResponseHeader.FirstOrDefault();
                    //    Console.WriteLine("Response variable" + id);
                    //}
                    //Console.ReadKey();
                }
            }
        }

        async static void PostRequest(String url)
        {
            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("query1","salim"),
                 new KeyValuePair<string, string>("query2","Bhonhariya"),

            };
            HttpContent q = new FormUrlEncodedContent(queries);
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.PostAsync(url, q))
                {
                    try
                    {
                        using (HttpContent content = response.Content)
                        {
                            string myContent = await content.ReadAsStringAsync();
                            HttpContentHeaders headers = content.Headers;

                            Console.Write(myContent);

                            Console.ReadKey();
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
