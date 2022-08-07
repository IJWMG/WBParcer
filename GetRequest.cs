using System.Net;
using Newtonsoft.Json;

namespace WBParcer
{
    public class GetRequest
    {
        HttpWebRequest request;
        string address;
        public string Response { get; set; }
        public string Accept { get; set; }
        public string Host { get; set; }
        public string Referer { get; set; }
        public string UserAgent { get; set; }
        public Root responseJSON;

        public Dictionary<string, string> Headers { get; set; }
        public GetRequest(string addr)
        {
            address = addr;
            Headers = new Dictionary<string, string>();
        }
        public void Start(CookieContainer container)
        {
            request = (HttpWebRequest)WebRequest.Create(address);
            request.Method = "Get";
            request.CookieContainer = container;
            request.Accept = Accept;
            request.Host = Host;
            request.Referer = Referer;
            request.UserAgent = UserAgent;



            foreach (var header in Headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var stream = response.GetResponseStream();
                if (stream != null)
                {
                    Response = new StreamReader(stream).ReadToEnd();
                    responseJSON = JsonConvert.DeserializeObject<Root>(Response);
                }
            }
            catch (Exception e) { Console.WriteLine(e); }
        }
    }
}