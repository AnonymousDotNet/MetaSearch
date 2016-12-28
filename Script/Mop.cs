using ClawlerVarietyShow.Utility;
using Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSearch.Script
{
    class Mop
    {
        public static void Start()
        {
            List<string> pages = new List<string>();
            for (int i = 1; i < 2; i++)
            {
                ParseUrl(pages, "http://search.mop.com/cse/search?q=%E5%BD%AD%E5%B7%9E%E7%9F%B3%E5%8C%96&click=" + i + "&s=10030666497398670337&nsid=");
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
                List<string> liststring = XpathUtil.GetAttributes(hn.DocumentNode, "//h3[@class='c-title']/a", "href");
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
            vd.Title = XpathUtil.GetText(hn.DocumentNode, "//h1[@class='c333 subTitle']");
            vd.Author = XpathUtil.GetText(hn.DocumentNode, "//div[@class='r-landlordMsg p20 clearfix']/div[2]/a");
            vd.Time = XpathUtil.GetText(hn.DocumentNode, "//span[@class='c999 mr15']");
            vd.Content = XpathUtil.GetText(hn.DocumentNode, "//div[@class='article-cont p20 c666']");
            vd.Source = "猫扑";
            vdList.Add(vd);
            return vdList;
        }
    }
}
