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
            List<VideoData> vdList = new List<VideoData>();
            for (int i = 1; i < 2; i++)
            {
                ParseUrl(vdList, "http://search.mop.com/cse/search?q=%E5%BD%AD%E5%B7%9E%E7%9F%B3%E5%8C%96&click=" + i + "&s=10030666497398670337&nsid=");
            }
            DataToExcel.CreateExcel("猫扑社区", vdList);
        }
        public static void ParseUrl(List<VideoData> vdList, string Url)
        {
            try
            {
                string url = "";
                string html = Http.Downloader.Download(Url);
                HtmlDocument hn = new HtmlDocument();
                hn.LoadHtml(html);
                List<string> liststring = XpathUtil.GetAttributes(hn.DocumentNode, "//h3[@class='c-title']/a", "href");
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
            vd.Title = XpathUtil.GetText(hn.DocumentNode, "//h1[@class='c333 subTitle']").Replace("&nbsp;", "");
            vd.Author = XpathUtil.GetText(hn.DocumentNode, "//div[@class='r-landlordMsg p20 clearfix']/div[2]/a");
            vd.Time = XpathUtil.GetText(hn.DocumentNode, "//span[@class='c999 mr15']");
            vd.Content = XpathUtil.GetText(hn.DocumentNode, "//div[@class='article-cont p20 c666']").Replace("\r", "").Replace("\n", "");
            vd.Source = "猫扑社区";
            vdList.Add(vd);
            return vdList;
        }
    }
}
