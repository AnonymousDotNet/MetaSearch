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
            List<VideoData> vdList = new List<VideoData>();

            ParseUrl(vdList, "http://search.home.news.cn/forumbookSearch.do?sw=%E5%BD%AD%E5%B7%9E%E7%9F%B3%E5%8C%96&srchType=1");

            DataToExcel.CreateExcel("发展论坛", vdList);
        }
        public static void ParseUrl(List<VideoData> vdList, string Url)
        {
            try
            {
                string url = "";
                string html = Http.Downloader.Download(Url);
                HtmlDocument hn = new HtmlDocument();
                hn.LoadHtml(html);
                List<string> liststring = XpathUtil.GetAttributes(hn.DocumentNode, "//td[@width='614']/a", "href");
                foreach (var item in liststring)
                {
                    url = item;

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
            vd.Title = XpathUtil.GetText(hn.DocumentNode, "//td[@class='biaoti']");
            vd.Author = XpathUtil.GetText(hn.DocumentNode, "//span[@class='zuozhe01']/a");
            //vd.Time = XpathUtil.GetText(hn.DocumentNode, "//td[@class='zuozhe']");
            vd.Time = RegexUtil.MatchText(XpathUtil.GetText(hn.DocumentNode, "//td[@class='zuozhe']"), "\\d{4}-\\d{2}-\\d{2} \\d{2}:\\d{2}");


            vd.Content = XpathUtil.GetText(hn.DocumentNode, "//td[@width='941']/p").Replace("&nbsp;", "").Replace("\r","").Replace("\n","");
            vd.Source = "发展论坛";
            vdList.Add(vd);
            return vdList;
        }
    }
}
