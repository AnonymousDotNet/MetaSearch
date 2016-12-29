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
            List<VideoData> vdList = new List<VideoData>();

            ParseUrl(vdList, "http://bbs1.people.com.cn/quickSearch.do?field=title&threadtype=1&content=%E5%BD%AD%E5%B7%9E%E7%9F%B3%E5%8C%96");

            DataToExcel.CreateExcel("强国论坛", vdList);
        }
        public static void ParseUrl(List<VideoData> vdList, string Url)
        {
            try
            {
                string url = "";
                string html = Http.Downloader.Download(Url);
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
                    Uri uri = new Uri(url);

                    GetNeedData(uri, vdList);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private static List<VideoData> GetNeedData(Uri uri, List<VideoData> vdList)
        {
            VideoData vd = new VideoData();
            string html = Http.Downloader.Download(uri.AbsoluteUri);
            HtmlDocument hn = new HtmlDocument();
            hn.LoadHtml(html);
            vd.Url = uri.AbsoluteUri;
            string contenturl = XpathUtil.GetAttribute(hn.DocumentNode, "//div[@class='article scrollFlag']", "content_path");
            string content = Http.Downloader.Download(contenturl, Encoding.GetEncoding("UTF-8"));
            vd.Content = content;
            //vd.Content = XpathUtil.GetText(hn.DocumentNode, "//div[@class='d_post_content j_d_post_content ']");
            vd.Title = XpathUtil.GetText(hn.DocumentNode, "//div[@class='navBar']/h2");
            vd.Author = XpathUtil.GetText(hn.DocumentNode, "//div[@class='clearfix']/a|//div[@class='clearfix']/span[@class='float_l']");
            vd.Time = RegexUtil.MatchText(XpathUtil.GetText(hn.DocumentNode, "//span[@class='float_l mT10']"), "\\d{4}-\\d{2}-\\d{2} \\d{2}:\\d{2}");
            vd.Source = "强国论坛";
            vdList.Add(vd);
            return vdList;
        }
    }
}
