using ClawlerVarietyShow.Utility;
using Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSearch.Script
{
    class FaZhanForum
    {
        public static void Start()
        {
            List<string> pages = new List<string>();
            //for (int i = 1; i < 2; i++)
            //{
            ParseUrl(pages, "http://search.home.news.cn/forumbookSearch.do?sw=%E5%BD%AD%E5%B7%9E%E7%9F%B3%E5%8C%96&srchType=1");
            //}
        }
        public static void ParseUrl(List<string> urls, string home)
        {
            try
            {
                string url = "";
                string html = Http.Downloader.Download(home);
                HtmlDocument hn = new HtmlDocument();
                hn.LoadHtml(html);
                List<string> liststring = XpathUtil.GetAttributes(hn.DocumentNode, "//td[@width='614']/a", "href");
                foreach (var item in liststring)
                {
                    //if (!item.Contains("http://bbs1.people.com"))
                    //{
                    //    url = "http://bbs1.people.com.cn" + item;
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
            vd.Title = XpathUtil.GetText(hn.DocumentNode, "//td[@class='biaoti']");
            vd.Author = XpathUtil.GetText(hn.DocumentNode, "//span[@class='zuozhe01']/a");
            vd.Time = XpathUtil.GetText(hn.DocumentNode, "//td[@class='zuozhe']");
            vd.Content = XpathUtil.GetText(hn.DocumentNode, "//td[@width='941']/p");
            vd.Source = "发展论坛";
            vdList.Add(vd);
            return vdList;
        }
    }
}
