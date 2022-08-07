
namespace WBParcer
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "Keys.txt";
            RequestToWB forRequestsYouMake = new RequestToWB();
            Dictionary<string, Data> productsByKeys = new Dictionary<string, Data>();

            string itemName;
            StreamReader reader = new StreamReader(path);
            while (!String.IsNullOrEmpty((itemName = reader.ReadLine())?.Trim()))
            {
                productsByKeys.Add(itemName, forRequestsYouMake.GetResult(itemName));
            }
            ExcelGenetator genetator = new ExcelGenetator();
            genetator.Generate(productsByKeys);
        }
    }
}