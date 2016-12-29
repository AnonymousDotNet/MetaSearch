using ClawlerVarietyShow.Utility;
using Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaSearch.Script
{
    class People
    {
        public static void Start()
        {
            List<string> pages = new List<string>();
            //for (int i = 1; i < 2; i++)
            //{
            ParseUrl(pages, "http://bbs1.people.com.cn/quickSearch.do?field=title&threadtype=1&content=%E5%BD%AD%E5%B7%9E%E7%9F%B3%E5%8C%96");
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
                List<string> liststring = XpathUtil.GetAttributes(hn.DocumentNode, "//p[@class='treeTitle']/a", "href");
                foreach (var item in liststring)
                {
                    if (!item.Contains("http://bbs1.people.com"))
                    {
                        url = "http://bbs1.people.com.cn" + item;
                    }
                    else
                    {
                        url = item;
                    }
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
            vd.Content = XpathUtil.GetText(hn.DocumentNode, "//div[@class='d_post_content j_d_post_content ']");
            vd.Title = XpathUtil.GetText(hn.DocumentNode, "//div[@class='core_title_wrap_bright clearfix']/h3");
            vd.Author = XpathUtil.GetText(hn.DocumentNode, "//li[@class='d_name']/a");
            vd.Time = XpathUtil.GetText(hn.DocumentNode, "//div[@class='post-tail-wrap']/span[4]");
            vd.Source = "百度贴吧";
            vdList.Add(vd);
            return vdList;
        }
    }
}
