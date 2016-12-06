using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace DocBuilder
{
    /// <summary>
    ///Code
    ///Name
    ///Open
    ///Last
    ///Current
    ///Max
    ///Min
    ///Buy
    ///Sell
    ///Turnover
    ///Amount
    ///Buy1
    ///BuyPrice1
    ///Buy2
    ///BuyPrice2
    ///Buy3
    ///BuyPrice3
    ///Buy4
    ///BuyPrice4
    ///Buy5
    ///BuyPrice5
    ///Sell1
    ///SellPrice1
    ///Sell2
    ///SellPrice2
    ///Sell3
    ///SellPrice3
    ///Sell4
    ///SellPrice4
    ///Sell5
    ///SellPrice5
    ///Date
    ///Time
    ///MillSecond
    ///Remark
    /// </summary>
    public class StockPrice
    {
        #region ctor
        public string Code { get; set; }
        public string Name { get; set; }
        public string Open { get; set; }
        public string Last { get; set; }
        public string Current { get; set; }
        public string Max { get; set; }
        public string Min { get; set; }
        public string Buy { get; set; }
        public string Sell { get; set; }
        public string Turnover { get; set; }
        public string Amount { get; set; }
        public string Buy1 { get; set; }
        public string BuyPrice1 { get; set; }
        public string Buy2 { get; set; }
        public string BuyPrice2 { get; set; }
        public string Buy3 { get; set; }
        public string BuyPrice3 { get; set; }
        public string Buy4 { get; set; }
        public string BuyPrice4 { get; set; }
        public string Buy5 { get; set; }
        public string BuyPrice5 { get; set; }
        public string Sell1 { get; set; }
        public string SellPrice1 { get; set; }
        public string Sell2 { get; set; }
        public string SellPrice2 { get; set; }
        public string Sell3 { get; set; }
        public string SellPrice3 { get; set; }
        public string Sell4 { get; set; }
        public string SellPrice4 { get; set; }
        public string Sell5 { get; set; }
        public string SellPrice5 { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string MillSecond { get; set; }
        public string Remark { get; set; }

        private static readonly string[] feilds = new string[]{
            "Code",
            "Name",
            "Open",
            "Last",
            "Current",
            "Max",
            "Min",
            "Buy",
            "Sell",
            "Turnover",
            "Amount",
            "Buy1",
            "BuyPrice1",
            "Buy2",
            "BuyPrice2",
            "Buy3",
            "BuyPrice3",
            "Buy4",
            "BuyPrice4",
            "Buy5",
            "BuyPrice5",
            "Sell1",
            "SellPrice1",
            "Sell2",
            "SellPrice2",
            "Sell3",
            "SellPrice3",
            "Sell4",
            "SellPrice4",
            "Sell5",
            "SellPrice5",
            "Date",
            "Time",
            "MillSecond",
            "Remark"
            };
        StockPrice() { }
        #endregion
        public static StockPrice NewStockPrice(string input)
        {
            StringBuilder temp = new StringBuilder();
            var values = input.Split('\"', ',');
            temp.Append("{");
            temp.Append(string.Format("\"{0}\":\"{1}\",", feilds[0], values[0].Replace("var hq_str_", "").Replace("=", "")));
            for (int i = 1; i < values.Length - 1; i++)
            {
                temp.Append(string.Format("\"{0}\":\"{1}\",", feilds[i], values[i].Trim()));
            }
            temp.Append("\"Remark\":\"\"}");
            return JsonConvert.DeserializeObject<StockPrice>(temp.ToString());
        }
    }
    /// <summary>
    /// Date
    /// Open
    /// Max
    /// Min
    /// Close
    /// Turnover
    /// Amount
    /// </summary>
    public class DailyPrice
    {
        #region ctor
        private static readonly string[] feilds = new string[] {"Date",
"Open",
"Max",
"Min",
"Close",
"Turnover",
"Amount"};
        public string Date { get; set; }
        public string Open { get; set; }
        public string Max { get; set; }
        public string Min { get; set; }
        public string Close { get; set; }
        public string Turnover { get; set; }
        public string Amount { get; set; }
        DailyPrice() { }
        #endregion
        public static DailyPrice NewDailyPrice(string input)
        {
            try
            {
                StringBuilder temp = new StringBuilder();
                var values = input.Split(',');
                temp.Append("{");
                for (int i = 0; i < values.Length; i++)
                {
                    temp.Append(string.Format("\"{0}\":\"{1}\",", feilds[i], values[i].Trim()));
                }
                temp.Append("\"Remark\":\"\"}");
                return JsonConvert.DeserializeObject<DailyPrice>(temp.ToString());
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string Code { get; set; }
    }

    public static class HtmlHelper
    {
        public static string GetTitle(this HtmlDocument doc)
        {
            var titleNode = doc.DocumentNode.SelectNodes("/html/head/title").FirstOrDefault();
            if (titleNode == null)
            {
                return "";
            }
            return titleNode.InnerText;
        }
        public static string GetDescription(this HtmlDocument doc)
        {
            var descriptionNode = doc.DocumentNode.SelectNodes("/html/head/meta").Where(m => m.Attributes["name"].Value == "description").FirstOrDefault();
            if (descriptionNode == null)
            {
                return "";
            }
            return descriptionNode.Attributes["content"].Value;
        }
        public static List<string> GetAllLinks(this HtmlDocument doc, string path)
        {
            var list = new List<string>();
            foreach (var item in doc.DocumentNode.SelectNodes("//a[contains(@href)]"))
            {
                if (item.Attributes["href"].Value.StartsWith(path))
                {
                    list.Add(item.Attributes["href"].Value);
                }
            }

            return list;
        }
        public static void GetUrl(string url)
        {
            new UrlBuilder(url, 1).Run();
            foreach (var doc in UrlBuilder.list.Values)
            {

                var title = doc.GetTitle();
                var description = doc.GetDescription();
                string fileName = Path.Combine(Environment.CurrentDirectory, "Pages", url.GetHashCode().ToString() + ".htm");


                foreach (var item in doc.DocumentNode.SelectNodes("//a[contains(@href)]"))
                {
                    if (item == null)
                    {
                        continue;
                    }
                    if (item.Name == "script")
                    {
                        item.Remove();
                    }
                    if (item.Name == "")
                    { }
                }

                doc.Save(fileName);
            }
        }
        public static string DownLoadSource(string url, string type)
        {
            string fileName = Path.Combine(Environment.CurrentDirectory, "Content", type, url.GetHashCode().ToString());
            try
            {
                WebClient client = new WebClient();
                //client.Proxy =  WebProxyHelper.CreateWebProxy();
                client.DownloadFile(url, fileName);
            }
            catch (Exception ex)
            {
            }
            return fileName;
        }
    }

    public class UrlBuilder
    {
        public static Dictionary<string, HtmlDocument> list = new Dictionary<string, HtmlDocument>();

        HtmlWeb web = new HtmlWeb();
        public Uri OrgianlPage { get; set; }

        public int Deep { get; set; }

        public UrlBuilder(string url, int deep = 0)
        {
            this.OrgianlPage = new Uri(url);
            this.Deep = deep;
        }

        public void Run()
        {
            if (!list.ContainsKey(this.OrgianlPage.ToString()))
            {
                list.Add(this.OrgianlPage.ToString(), web.Load(this.OrgianlPage.ToString()));
            }
            if (this.Deep > 0)
            {
                foreach (var item in list[this.OrgianlPage.ToString()].GetAllLinks(this.OrgianlPage.GetLeftPart(UriPartial.Path)))
                {
                    new UrlBuilder(item, this.Deep - 1).Run();
                }
            }
        }
    }
}