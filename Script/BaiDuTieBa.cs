using ClawlerVarietyShow.Utility;
using Http;
using Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MetaSearch
{
    class BaiDuTieBa
    {
        public static void Start()
        {
            List<VideoData> vdList = new List<VideoData>();
            for (int i = 1; i < 42; i++)
            {
                ParseUrl(vdList, "http://tieba.baidu.com/f/search/res?isnew=1&kw=&qw=%C5%ED%D6%DD%CA%AF%BB%AF&rn=10&un=&only_thread=1&sm=1&sd=&ed=&pn=" + i);
            }
            DataToExcel.CreateExcel("百度贴吧", vdList);

        }


        public static void ParseUrl(List<VideoData> vdList, string Url)
        {
            try
            {
                string url = "";
                string html = Http.Downloader.Download(Url);
                HtmlDocument hn = new HtmlDocument();
                hn.LoadHtml(html);
                List<string> liststring = XpathUtil.GetAttributes(hn.DocumentNode, "//span[@class='p_title']/a", "href");
                foreach (var item in liststring)
                {
                    if (!item.Contains("http://tieba.baidu"))
                    {
                        url = "http://tieba.baidu.com" + item;
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
            string html = Downloader.Download(uri.AbsoluteUri);
            HtmlDocument hn = new HtmlDocument();
            hn.LoadHtml(html);


            vd.Url = uri.AbsoluteUri;

            //vd.Content = XpathUtil.GetText(hn.DocumentNode, "//div[@class='l_post j_l_post l_post_bright noborder ']//cc");
            vd.Content = XpathUtil.GetText(hn.DocumentNode, "//div[@class='l_post j_l_post l_post_bright noborder ']//div[contains(@id,'post_content_')]|//div[@class='l_post l_post_bright j_l_post clearfix  ']//div[contains(@id,'post_content_')]");

            //vd.Title = XpathUtil.GetText(hn.DocumentNode, "//h1[@class='core_title_txt  ']");
            vd.Title = XpathUtil.GetText(hn.DocumentNode, "//h1|//div[@id='j_core_title_wrap']//h3");

            //vd.Author = XpathUtil.GetText(hn.DocumentNode, "//li[@class='d_name']/a");
            vd.Author = RegexUtil.RemoveNoise(XpathUtil.GetText(hn.DocumentNode, "//li[@class='d_name']"), "\\s");

            //vd.Time = XpathUtil.GetText(hn.DocumentNode, "//div[@class='core_reply_tail ']//ul[@class='p_tail']");
            vd.Time = RegexUtil.MatchText(html, "\\d{4}-\\d{2}-\\d{2} \\d{2}:\\d{2}");

            vd.Source = "百度贴吧";

            if (string.IsNullOrEmpty(vd.Content))
            {
                vd.Content = vd.Title;
            }
            else
            {
                vd.Content = vd.Content.Replace("&#xFFFF;", "");
            }

            if (string.IsNullOrEmpty(vd.Time))
            {
                vd.Time = RegexUtil.MatchText(html, "\"date\":\"(?<time>\\d+-\\d+-\\d+ \\d+:\\d+)[\\s\\S]+?floor\":\\d+,|&quot;date&quot;:&quot;(?<time>\\d{4}-\\d{2}-\\d{2} \\d{2}:\\d{2})&quot;,", "time");
            }

            vdList.Add(vd);
            return vdList;
        }
    }
}
