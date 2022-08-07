using System.Net;
using System.Web;


namespace WBParcer
{

    public class RequestToWB
    {
        private CookieContainer container = new CookieContainer();
        private string itemName;

        public Data GetResult(string name)
        {
            itemName = name;
            string codedItemName = HttpUtility.UrlEncode(itemName);
            GetRequest request = new GetRequest($"https://search.wb.ru/exactmatch/ru/common/v4/search?appType=1&couponsGeo=12,3,18,15,21&curr=rub&dest=-1029256,-102269,-1278703,-1255563&emp=0&lang=ru&locale=ru&pricemarginCoeff=1.0&query={itemName}&reg=1&regions=68,64,83,4,38,80,33,70,82,86,75,30,69,22,66,31,40,1,48,71&resultset=catalog&sort=popular&spp=21&suppressSpellcheck=false");

            request.Accept = "*/*";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.0.0 Safari/537.36";
            request.Referer = $"https://www.wildberries.ru/catalog/0/search.aspx?sort=popular&search={codedItemName}";
            request.Headers.Add("Origin", "https://www.wildberries.ru");
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.0.0 Safari/537.36");
            request.Headers.Add("sec-ch-ua", $"\"Chromium\";v=\"104\", \" Not A;Brand\";v=\"99\", \"Google Chrome\";v=\"104\"");
            request.Headers.Add("sec-ch-ua-mobile", "?0");
            request.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
            request.Headers.Add("Sec-Fetch-Dest", "empty");
            request.Headers.Add("Sec-Fetch-Mode", "cors");
            request.Headers.Add("Sec-Fetch-Site", "cross-site");
            request.Host = "search.wb.ru";

            request.Start(container);
            return request.responseJSON.data;
        }
    }
}
