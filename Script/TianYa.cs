using ClawlerVarietyShow.Utility;
using Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSearch.Script
{
    class TianYa
    {
        public static void Start()
        {
            List<string> pages = new List<string>();
            for (int i = 1; i < 76; i++)
            {
                ParseUrl(pages, "http://search.tianya.cn/bbs?q=%E5%BD%AD%E5%B7%9E%E7%9F%B3%E5%8C%96&pn=" + i);
            }
        }
        public static void ParseUrl(List<string> urls, string home)
        {
            try
            {
                string url = "";
                string html = Http.Downloader.Download(home);
                HtmlDocument hn = new HtmlDocument();
                hn.LoadHtml(html);
                List<string> liststring = XpathUtil.GetAttributes(hn.DocumentNode, "//h3/a", "href");
                foreach (var item in liststring)
                {
                    //if (!item.Contains("http://tieba.baidu"))
                    //{
                    //    url = "http://tieba.baidu.com" + item;
                    //}
                    //else
                    //{
                    //    url = item;
                    //}
                    Uri uri = new Uri(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private static List<VideoData> GetNeedData(string url, List<VideoData> vdList)
        {
            VideoData vd = new VideoData();
            string html = Http.Downloader.Download(url);
            HtmlDocument hn = new HtmlDocument();
            hn.LoadHtml(html);
            vd.Url = url;
            vd.Title = XpathUtil.GetText(hn.DocumentNode, "//h1[@class='atl-title']/span/span");
            vd.Author = XpathUtil.GetText(hn.DocumentNode, "//div[@class='atl-info']/span/a");
            vd.Time = XpathUtil.GetText(hn.DocumentNode, "//div[@class='atl-info']/span[2]");
            vd.Content = XpathUtil.GetText(hn.DocumentNode, "//div[@class='bbs-content clearfix']");
            vd.Source = "天涯社区";
            vdList.Add(vd);
            return vdList;
        }
    }
}
